﻿using HugoLandEditeur.Presentation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TP01_Library;
using TP01_Library.Controllers;
using System.IO;
using System.Linq;

namespace HugoLandEditeur
{
    public partial class frmMain : Form
    {
        // Variable globale interne
        public static CompteJoueur currentJoueur { get; set; }

        private CMap m_Map;
        private CTileLibrary m_TileLibrary;
        private int m_XSel;
        private int m_YSel;
        private int m_TilesHoriz;
        private System.Windows.Forms.Timer timer1;
        private int m_TilesVert;
        private bool m_bRefresh;
        private bool m_bResize;
        private int m_Zoom;
        private Rectangle m_TileRect;
        private Rectangle m_LibRect;
        private int m_ActiveXIndex;
        private int m_ActiveYIndex;
        private int m_ActiveTileID;
        private int m_ActiveTileXIndex;
        private int m_ActiveTileYIndex;
        private MondeController mondeCTRL = new MondeController();
        private CompteJoueurController compteJoueurCTRL = new CompteJoueurController();
        private Constantes CONSTANTES = new Constantes();

        /// <summary>
        /// Summary description for Form1.
        /// </summary>
        public struct ComboItem
        {
            public string Text;
            public int Value;

            public ComboItem(string text, int val)
            {
                Text = text;
                Value = val;
            }

            public override string ToString()
            {
                return Text;
            }
        };

        public frmMain()
        {
            #region Load la BD
            //StreamReader sr = new StreamReader(@"gamedata\AllTilesLookups.csv");
            //ObjetMondeController ctrlObj = new ObjetMondeController();
            //MonstreController ctrlMonstre = new MonstreController();
            //ItemController ctrlItem = new ItemController();

            //Monde monde;
            //using (HugoLandContext context = new HugoLandContext())
            //{
            //    monde = context.Mondes.FirstOrDefault();
            //}

            //string line;
            //while ((line = sr.ReadLine()) != null)
            //{
            //    //separate out the elements of the
            //    string[] elements = line.Split(',');

            //    TypeTile tile = (TypeTile)Enum.Parse(typeof(TypeTile), elements[elements.Length - 1], true);
            //    int x = int.Parse(elements[4]);
            //    int y = int.Parse(elements[5]);
            //    int imageId = int.Parse(elements[1]);

            //    switch (tile)
            //    {
            //        case TypeTile.ObjetMonde:
            //            ctrlObj.AjouterObjetMonde(monde.Id, elements[0], x, y, (int)tile, imageId);
            //            break;
            //        case TypeTile.Monstre:
            //            ctrlMonstre.AjouterMonstre(monde, x, y, elements[0], imageId);
            //            break;
            //        case TypeTile.Item:
            //            ctrlItem.AjouterItems(elements[0], elements[2], x, y, imageId, monde.Id);
            //            break;
            //        default:
            //            break;
            //    }
            //}
            #endregion

            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /* -------------------------------------------------------------- *\
        frmMain_Load()
        - Main Form Initialization
    \* -------------------------------------------------------------- */
        private void frmMain_Load(object sender, System.EventArgs e)
        {
            // Nouvelle instance de map
            m_Map = new CMap();

            // Instance de base à l'aide du ctor pour une tuile dans la map
            m_TileLibrary = new CTileLibrary();

            // Insère la tuile dans celle de la map générer plus haut
            m_Map.TileLibrary = m_TileLibrary;

            // Position de où la map ira
            picMap.Parent = picEditArea;
            picMap.Left = 0;
            picMap.Top = 0;

            // Le look, taille des tuiles
            picTiles.Parent = picEditSel;
            picTiles.Width = m_TileLibrary.Width * csteApplication.TILE_WIDTH_IN_IMAGE;
            picTiles.Height = m_TileLibrary.Height * csteApplication.TILE_HEIGHT_IN_IMAGE;
            picTiles.Left = 0;
            picTiles.Top = 0;

            // Vertical Scroll bar
            vscMap.Minimum = 0;
            vscMap.Maximum = m_Map.Height;
            m_YSel = 0;

            // Horizontal Scroll bar
            hscMap.Minimum = 0;
            hscMap.Maximum = m_Map.Width;
            m_XSel = 0;

            m_bRefresh = true;
            m_bResize = true;
            timer1.Enabled = true;
            m_Zoom = csteApplication.ZOOM;

            m_TileRect = new Rectangle(-1, -1, -1, -1);
            m_LibRect = new Rectangle(-1, -1, -1, -1);
            m_ActiveTileID = 32;

            //dlgLoadMap.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath) + "\\maps\\";
            //dlgSaveMap.InitialDirectory = dlgLoadMap.InitialDirectory;
            m_bOpen = false;
            m_MenuLogic();
            //tmrLoad.Enabled = true;

            m_pen = new Pen(Color.Orange, 4);
            m_brush = new SolidBrush(Color.FromArgb(160, 249, 174, 55));
            m_brush2 = new SolidBrush(Color.FromArgb(160, 255, 0, 0));

            m_bDrawTileRect = false;
            m_bDrawMapRect = false;

            cboZoom.Left = 270;
            cboZoom.Top = 2;
            cboZoom.Items.Add(new ComboItem("1X", 1));
            cboZoom.Items.Add(new ComboItem("2X", 2));
            cboZoom.Items.Add(new ComboItem("4X", 4));
            cboZoom.Items.Add(new ComboItem("8X", 8));
            cboZoom.Items.Add(new ComboItem("16X", 16));
            cboZoom.SelectedIndex = 1;
            cboZoom.DropDownStyle = ComboBoxStyle.DropDownList;

            lblZoom.Left = 180;
            lblZoom.Top = 2;

            tbMain.Controls.Add(lblZoom);
            tbMain.Controls.Add(cboZoom);
        }

        /* -------------------------------------------------------------- *\
        Menus
    \* -------------------------------------------------------------- */
        #region Menu Code

        private void mnuFileExit_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void mnuHelpAbout_Click(object sender, System.EventArgs e)
        {
            frmAbout f = new frmAbout();
            f.ShowDialog(this);
        }

        private void mnuZoomX1_Click(object sender, System.EventArgs e)
        {
            ResetScroll();
            m_Zoom = 1;
            m_bResize = true;
        }

        private void mnuZoomX2_Click(object sender, System.EventArgs e)
        {
            ResetScroll();
            m_Zoom = 2;
            m_bResize = true;
        }

        private void mnuZoomX4_Click(object sender, System.EventArgs e)
        {
            ResetScroll();
            m_Zoom = 4;
            m_bResize = true;
        }

        private void mnuZoomX8_Click(object sender, System.EventArgs e)
        {
            ResetScroll();
            m_Zoom = 8;
            m_bResize = true;
        }

        private void mnuZoomX16_Click(object sender, System.EventArgs e)
        {
            ResetScroll();
            m_Zoom = 16;
            m_bResize = true;
        }

        /* -------------------------------------------------------------- *\
            vscMap_Scroll()
            - vertical scroll bar for map editor window
        \* -------------------------------------------------------------- */
        private void vscMap_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
        {
            m_YSel = e.NewValue;
            if (m_bOpen)
                picMap.Refresh();
        }

        /* -------------------------------------------------------------- *\
            hscMap_Scroll()
            - horizontal scroll bar for map editor window
        \* -------------------------------------------------------------- */
        private void hscMap_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
        {
            m_XSel = e.NewValue;
            if (m_bOpen)
                picMap.Refresh();
        }

        /* -------------------------------------------------------------- *\
            picEditArea_Resize()

            - resize event for the parent of the map. The edit area is
              auto-sized to the space not taken by the lower and right
              panes.
        \* -------------------------------------------------------------- */
        private void picEditArea_Resize(object sender, System.EventArgs e)
        {
            if (m_bOpen)
            {
                m_XSel = 0;
                hscMap.Value = m_XSel;
                m_YSel = 0;
                vscMap.Value = m_YSel;
                m_bResize = true;
            }
        }

        /* -------------------------------------------------------------- *\
            timer1_Tick()

            - I'm not sure if this is necessary or not, but I was having
              difficulty updating things correctly due to timing of resizing
              items or updating scrolls and their values not getting set
              until after the event already occurred... so I'm setting
              flags instead.
        \* -------------------------------------------------------------- */
        private void timer1_Tick(object sender, System.EventArgs e)
        {
            if (m_bRefresh)
            {
                m_bRefresh = false;
                picMap.Refresh();
            }
            if (m_bResize)
            {
                m_bResize = false;
                m_ResizeMap();
            }
            if (m_bRefreshLib)
            {
                m_bRefreshLib = false;
                picTiles.Refresh();
            }
        }

        /* -------------------------------------------------------------- *\
            picMap_Paint()

            - This is where the Map picture box is painted to.
              This event happens when Refresh() is called or any section
              of the picture box is invalidated (i.e. covering up part of
              the picture box with another windows and then moving it away)
        \* -------------------------------------------------------------- */

        private void picMap_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (m_bOpen)
            {
                if (m_XSel < 0)
                    m_XSel = 0;
                if (m_YSel < 0)
                    m_YSel = 0;
                m_Map.Draw(e.Graphics, e.ClipRectangle, m_XSel, m_YSel);

                if (m_bDrawMapRect)
                    e.Graphics.FillRectangle(m_brush, m_TileRect);
            }
        }

        /* -------------------------------------------------------------- *\
            m_ResizeMap()

            - Takes care of Zoom, scroll and visible area logic.
        \* -------------------------------------------------------------- */

        private void m_ResizeMap()
        {
            int xpos, ypos;
            int nWidth = (vscMap.Left - 0); //picEditArea.Left);
            int AvailableWidth = nWidth - (2 * csteApplication.BUFFER_WIDTH);
            m_TilesHoriz = AvailableWidth / (m_Zoom * csteApplication.TILE_WIDTH_IN_MAP);
            int nMapWidth = m_TilesHoriz * csteApplication.TILE_WIDTH_IN_MAP * m_Zoom;
            int BorderX = (nWidth - nMapWidth) / 2;

            int nHeight = (hscMap.Top - 0); //picEditArea.Top);
            int AvailableHeight = nHeight - 2 * csteApplication.BUFFER_HEIGHT;
            m_TilesVert = AvailableHeight / (m_Zoom * csteApplication.TILE_HEIGHT_IN_MAP);
            int nMapHeight = m_TilesVert * csteApplication.TILE_HEIGHT_IN_MAP * m_Zoom;
            int BorderY = (nHeight - nMapHeight) / 2;

            PrintDebug("AvailableHeight = " + AvailableHeight.ToString());
            PrintDebug("BorderY = " + BorderY.ToString());
            PrintDebug("AvailableWidth = " + AvailableWidth.ToString());
            PrintDebug("BorderX = " + BorderX.ToString());

            m_Map.OffsetX = 0; //BorderX;
            m_Map.OffsetY = 0; //BorderY;
            m_Map.Zoom = m_Zoom;

            if (m_TilesHoriz < m_Map.Width)
            {
                //xpos = 16;
                xpos = 16 + (AvailableWidth - nMapWidth) / 2;
                m_Map.TilesHoriz = m_TilesHoriz;
                hscMap.Maximum = m_Map.Width - m_TilesHoriz;
            }
            else
            {
                m_Map.TilesHoriz = m_Map.Width;
                nMapWidth = m_Map.Width * csteApplication.TILE_WIDTH_IN_MAP * m_Zoom;
                xpos = 16 + (AvailableWidth - nMapWidth) / 2;
                hscMap.Maximum = 0;
            }

            if (m_TilesVert < m_Map.Height)
            {
                //ypos = 32;
                ypos = 32 + (AvailableHeight - nMapHeight) / 2;
                m_Map.TilesVert = m_TilesVert;
                vscMap.Maximum = m_Map.Height - m_TilesVert;
            }
            else
            {
                m_Map.TilesVert = m_Map.Height;
                nMapHeight = m_Map.Height * csteApplication.TILE_HEIGHT_IN_MAP * m_Zoom;
                ypos = 32 + (AvailableHeight - nMapHeight) / 2;
                vscMap.Maximum = 0;
            }

            picMap.Location = new System.Drawing.Point(xpos, ypos);
            picMap.Size = new Size(nMapWidth, nMapHeight);

            m_bRefresh = true;
        }

        /* -------------------------------------------------------------- *\
            picMap_MouseMove()

            - Keeps track / translates coordinates to map tile to be
              updated if clicked on.
        \* -------------------------------------------------------------- */

        private void picMap_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.X < 0 || e.Y < 0)
                return;
            if (e.X < m_TileRect.Left || e.X > m_TileRect.Right || e.Y < m_TileRect.Top || e.Y > m_TileRect.Bottom)
            {
                m_bDrawMapRect = true;

                m_Map.PointToTile(e.X, e.Y, ref m_ActiveXIndex, ref m_ActiveYIndex);
                m_Map.PointToBoundingRect(e.X, e.Y, ref m_TileRect);
                m_ActiveXIndex += m_XSel;
                m_ActiveYIndex += m_YSel;

                m_bRefresh = true;

                PrintDebug("XIndex = " + m_ActiveXIndex.ToString() + " YIndex = " + m_ActiveYIndex.ToString());
            }
        }

        private void picTiles_MouseLeave(object sender, System.EventArgs e)
        {
            m_bDrawTileRect = false;
            m_LibRect.X = -1;
            m_LibRect.Y = -1;
            m_LibRect.Width = -1;
            m_LibRect.Height = -1;
            m_bRefreshLib = true;
        }

        private void picMap_MouseLeave(object sender, System.EventArgs e)
        {
            m_bDrawMapRect = false;
            m_TileRect.X = -1;
            m_TileRect.Y = -1;
            m_TileRect.Width = -1;
            m_TileRect.Height = -1;
            m_bRefresh = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ComboItem myItem;
            myItem = (ComboItem)cboZoom.SelectedItem;
            ResetScroll();
            m_Zoom = myItem.Value;
            m_bResize = true;
            picTiles.Focus();
        }

        private void mnuFileNew_Click(object sender, System.EventArgs e)
        {
            NewMap();
        }

        /// <summary>
        /// Description: ? Bouton existant pas placé?
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCreateNewUser_Click(object sender, EventArgs e)
        {
        }

        /* -------------------------------------------------------------- *\
            picTiles_Paint()

            - Paints the tile library at the bottom of the screen.
        \* -------------------------------------------------------------- */

        private void picTiles_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (m_TileLibrary != null)
            {
                m_TileLibrary.Draw(e.Graphics, e.ClipRectangle);
                if (m_bDrawTileRect)
                    e.Graphics.FillRectangle(m_brush2, m_LibRect);
            }
        }

        /* -------------------------------------------------------------- *\
            vscTiles_Scroll()

            - controls the tile library scroll / position
        \* -------------------------------------------------------------- */

        private void vscTiles_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
        {
            picTiles.Top = -e.NewValue;
        }

        /* -------------------------------------------------------------- *\
            picTiles_MouseMove()

            - Keeps track / translates coordinates to tilelibrary tile to be
              selected if clicked on.
        \* -------------------------------------------------------------- */

        private void picTiles_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.X < 0 || e.Y < 0)
                return;
            if (e.X < m_LibRect.Left || e.X > m_LibRect.Right || e.Y < m_LibRect.Top || e.Y > m_LibRect.Bottom)
            {
                m_bDrawTileRect = true;

                m_TileLibrary.PointToTile(e.X, e.Y, ref m_ActiveTileXIndex, ref m_ActiveTileYIndex);
                m_TileLibrary.PointToBoundingRect(e.X, e.Y, ref m_LibRect);

                m_bRefreshLib = true;

                PrintDebug("TileXIndex = " + m_ActiveTileXIndex.ToString() + " TileYIndex = " + m_ActiveTileYIndex.ToString());
                PrintDebug("X = " + e.X.ToString() + " Y = " + e.Y.ToString());
            }
        }

        /* -------------------------------------------------------------- *\
            ResetScroll()

            - Resets the scrollbar to 0.
              I'm not sure if this is necessary anymore.. I was trouble-
              shooting an odd issue.
        \* -------------------------------------------------------------- */

        private void ResetScroll()
        {
            vscMap.Value = 0;
            m_YSel = 0;
            hscMap.Value = 0;
            m_XSel = 0;
        }

        /* -------------------------------------------------------------- *\
            picActiveTile_Paint()

            - Displays the selected tile.
        \* -------------------------------------------------------------- */

        private void picActiveTile_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Rectangle destrect = new Rectangle(0, 0, picActiveTile.Width, picActiveTile.Height);
            m_TileLibrary.DrawTile(e.Graphics, m_ActiveTileID, destrect);
        }

        /// <summary>
        /// Description: Monde par défaut
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrLoad_Tick(object sender, System.EventArgs e)
        {
            tmrLoad.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            m_Map.Refresh();
            m_bOpen = true;
            m_bRefresh = true;
            picMap.Visible = true;
            m_MenuLogic();
            this.Cursor = Cursors.Default;
        }

        #endregion Menu Code

        #region Debug Code

        private void PrintDebug(String strDebug)
        {
            Console.WriteLine(strDebug);
        }

        #endregion Debug Code

        /// <summary>
        /// Où: File => Open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileOpen_Click(object sender, System.EventArgs e)
        {
            LoadMap();
        }

        /// <summary>
        /// Où: File => Save [lorsqu'une map à été créée]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileSave_Click(object sender, System.EventArgs e)
        {
            m_SaveMap();
        }

        /// <summary>
        /// Description: Gère les trois icones; la feuille avec un +, le dossier et le ?
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbMain_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            if (e.Button == tbbSave)
                m_SaveMap();
            if (e.Button == tbbOpen)
                LoadMap();
            else if (e.Button == tbbNew)
                NewMap();
        }

        /// <summary>
        /// Description: Télécharge un [Monde] et l'applique sur la map du frmMain
        /// Méthode: ListerMonde() => MondeController
        /// </summary>
        private void LoadMap()
        {
            int iResult = -1;
            frmListSelector f;
            DialogResult result;
            f = new frmListSelector();
            result = f.ShowDialog();

            if (result == DialogResult.OK)
            {
                Monde m = f.monde;
                m_bOpen = false;
                picMap.Visible = false;
                this.Cursor = Cursors.WaitCursor;

                try
                {
                    iResult = m_Map.Load(m);
                    if (iResult == 0)
                    {
                        m_bOpen = true;
                        m_bRefresh = true;
                        m_bResize = true;
                        picMap.Visible = true;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    Console.WriteLine("Error Loading...");

                }
            }

            m_MenuLogic();
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Description: Save la map courante [Monde]
        /// Détails: AjouterMonde et ModifierMonde => Appeler pas ici, mais dans le code de CMap.cs
        /// </summary>
        private void m_SaveMap()
        {
            frmSave f;
            DialogResult result;

            f = new frmSave(m_Map.currentMonde);

            result = f.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    m_Map.currentMonde.Description = f.Description;
                    m_Map.Save();
                }
                catch
                {
                    Console.WriteLine("Error Saving...");
                }
                m_MenuLogic();
                this.Cursor = Cursors.Default;
            }

        }

        /// <summary>
        /// Description: Gère la création d'une "map" (monde?)
        /// Détails: PAS D'AJOUT À LA BD
        /// </summary>
        private void NewMap()
        {
            // Variables locales
            // Créer un objet du form pour la taille de l'image
            frmNew f;
            DialogResult result;
            bool bResult;

            // Instanciation d'une nouvelle map
            f = new frmNew();
            f.MapWidth = m_Map.Width;
            f.MapHeight = m_Map.Height;

            // Demande la hauteur et largeur de la map
            result = f.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                m_bOpen = false;
                picMap.Visible = false;
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    // Création de la map avec les dimensions de bases, soit 32 par 32 [tiles]
                    bResult = m_Map.CreateNew(f.MapWidth, f.MapHeight, 32);
                    if (bResult)
                    {
                        m_bOpen = true;
                        m_bRefresh = true;
                        m_bResize = true;
                        picMap.Visible = true;
                    }
                }
                catch
                {
                    Console.WriteLine("Error Creating...");
                }
                m_MenuLogic();
                this.Cursor = Cursors.Default;
            }
        }

        /* -------------------------------------------------------------- *\
            m_MenuLogic()

            - Enables / Disables menus based on application status
        \* -------------------------------------------------------------- */

        private void m_MenuLogic()
        {
            bool bEnabled;

            bEnabled = m_bOpen;
            mnuFileSave.Enabled = bEnabled;
            mnuFileClose.Enabled = bEnabled;
            mnuZoom.Enabled = bEnabled;
            tbbSave.Enabled = bEnabled;
        }

        private void mnuSettings_Click(object sender, EventArgs e)
        {

        }

        private void mnuCreateUser_Click(object sender, EventArgs e)
        {
            var form2 = new frmCreateUser();
            form2.Show();
        }


        /* -------------------------------------------------------------- *\
            picMap_Click()

            - Plots the ActiveTile from the tile library to the selected
            tile location on the map.
        \* -------------------------------------------------------------- */

        private void picMap_Click(object sender, System.EventArgs e)
        {
            //Hugo: Modifier ici pour avoir le tile et le type
            // VP (06/03/2021) gestion dans CTileLibrary.cs
            m_Map.PlotTile(m_ActiveXIndex, m_ActiveYIndex, m_ActiveTileID);

            UpdateInfos();

            m_bRefresh = true;
        }


        /* -------------------------------------------------------------- *\
            picTiles_Click()

            - Selects the active tile ID
        \* -------------------------------------------------------------- */

        private void picTiles_Click(object sender, System.EventArgs e)
        {
            m_ActiveTileID = m_TileLibrary.TileToTileID(m_ActiveTileXIndex, m_ActiveTileYIndex);

            picActiveTile.Refresh();
        }

        /* -------------------------------------------------------------- *\
            
        ObjetMonde => lbl_Description
        Item => lbl_Description
        Monstre => lstB_Monstre
        Hero => lstB_Hero

        Needs:
        - Type de tuile (Enum)
        - Methode de clic
        - Controllers: ObjetMonde, Item, Monstre et Hero
        - Models: TypeTyle.cs

        \* -------------------------------------------------------------- */

        private void UpdateInfos()
        {
            if (m_Map.currTile != null)
            {
                lbl_InfoType.Text = m_Map.currTile.TypeObjet.ToString();
                lbl_InfoType.Visible = true;
                switch (m_Map.currTile.TypeObjet)
                {
                    case TypeTile.ObjetMonde:
                        //lstB_Hero.Visible = false;
                        lstB_Monstre.Visible = false;
                        lbl_Description.Visible = true;
                        btnReset.Visible = true;
                        lbl_Description.Text = m_Map.currTile.Name;

                        ResetInfos();
                        break;
                    case TypeTile.Item:
                        //lstB_Hero.Visible = false;
                        lstB_Monstre.Visible = false;
                        lbl_Description.Visible = true;
                        btnReset.Visible = true;
                        lbl_Description.Text = m_Map.currTile.Name;

                        ResetInfos();
                        break;
                    case TypeTile.Monstre:
                        //lstB_Hero.Visible = false;
                        lstB_Monstre.Visible = false;
                        lbl_Description.Visible = true;
                        btnReset.Visible = true;

                        lbl_Description.Text = m_Map.currTile.Name;

                        ResetInfos();
                        break;
                        //case TypeTile.ClasseHero:
                        //    lstB_Monstre.Visible = false;
                        //    lbl_Description.Visible = false;
                        //    lstB_Hero.Visible = true;
                        //    btnReset.Visible = true;

                        //    HeroController ctrl = new HeroController();
                        //    Hero currHero = ctrl.GetHero(Name);

                        //    lstB_Hero.Items.Add(currHero.NomHero);

                        //    break;
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            m_ActiveTileID = 32;
            ResetInfos();
        }

        private void ResetInfos()
        {
            picActiveTile.Refresh();
            lbl_Description.Refresh();
            lbl_InfoType.Refresh();
            lstB_Hero.Refresh();
            lstB_Monstre.Refresh();
        }

        private void menuAdmins_Click(object sender, EventArgs e)
        {
            var form2 = new frmAdminList();
            form2.Show();
        }
    }
}