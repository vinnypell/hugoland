using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using TP01_Library;
using TP01_Library.Controllers;

namespace HugoLandEditeur
{
    /// <summary>
    /// Summary description for CMap.
    /// </summary>
    public class CMap
    {
        private CTileLibrary m_TileLibrary;     // Reference to a Tile Library
        private int m_Width;			// map width (tiles)
        private int m_Height;			// map height (tiles)
        private int m_DefaultTileID;	// default tile id for outside normal bounds
        private int[,] m_Tiles;			// logical 2-D array for map building
        private Bitmap m_BackBuffer;		// Back Buffer for plotting graphical map data.. We will not store in picture box.
        private Graphics m_BackBufferDC;
        private int m_OffsetX;
        private int m_OffsetY;
        private int m_nTilesVert;
        private int m_nTilesHoriz;
        private int m_Zoom;

        private Tile m_currTile;
        private MondeController ctrl = new MondeController();

        private Monde m_currentMonde;
        public Monde currentMonde
        {
            get
            {
                return m_currentMonde;
            }
            set
            {
                m_currentMonde = value;
            }
        }

        // Map Width (in Tiles)
        public int Width
        {
            get
            {
                return m_Width;
            }
            set
            {
                m_Width = value;
            }
        }

        // Map Zoom (X)
        public int Zoom
        {
            get
            {
                return m_Zoom;
            }
            set
            {
                m_Zoom = value;
            }
        }

        // Map Height (in Tiles)
        public int Height
        {
            get
            {
                return m_Height;
            }
            set
            {
                m_Height = value;
            }
        }

        // Default Tile ID
        public int DefaultTileID
        {
            get
            {
                return m_DefaultTileID;
            }
            set
            {
                m_DefaultTileID = value;
            }
        }

        // Default Tile ID
        public CTileLibrary TileLibrary
        {
            set
            {
                m_TileLibrary = value;
            }
        }

        // OffsetX (pixels)
        public int OffsetX
        {
            set
            {
                m_OffsetX = value;
            }
        }

        // OffsetY (pixels)
        public int OffsetY
        {
            set
            {
                m_OffsetY = value;
            }
        }

        // TilesVert
        public int TilesVert
        {
            set
            {
                m_nTilesVert = value;
            }
        }

        // TilesHoriz
        public int TilesHoriz
        {
            set
            {
                m_nTilesHoriz = value;
            }
        }

        // Current Selected Tile
        public Tile currTile
        {
            get
            {
                return m_currTile;
            }
            set
            {
                m_currTile = value;
            }
        }

        public CMap()
        {
        }

        public void Refresh()
        {
            int i;
            int j;

            for (i = 0; i < m_Height; i++)
                for (j = 0; j < m_Width; j++)
                    m_TileLibrary.DrawTile(m_BackBufferDC, m_Tiles[i, j], j * csteApplication.TILE_WIDTH_IN_MAP, i * csteApplication.TILE_HEIGHT_IN_MAP);
        }

        public void Draw(Graphics pGraphics, Rectangle destRect, int TileX, int TileY)
        {
            int xindex = 0;
            int yindex = 0;
            int width;
            int height;

            width = destRect.Width / m_Zoom;
            height = destRect.Height / m_Zoom;

            PointToTile(destRect.X, destRect.Y, ref xindex, ref yindex);
            destRect.X = xindex * m_Zoom * csteApplication.TILE_WIDTH_IN_MAP;
            destRect.Y = yindex * m_Zoom * csteApplication.TILE_HEIGHT_IN_MAP;
            destRect.Width = (m_nTilesHoriz - xindex) * csteApplication.TILE_WIDTH_IN_MAP * m_Zoom;
            destRect.Height = (m_nTilesVert - yindex) * csteApplication.TILE_HEIGHT_IN_MAP * m_Zoom;

            Rectangle srcRect = new Rectangle((TileX + xindex) * csteApplication.TILE_WIDTH_IN_MAP, (TileY + yindex) * csteApplication.TILE_HEIGHT_IN_MAP, (m_nTilesHoriz - xindex) * csteApplication.TILE_WIDTH_IN_MAP, (m_nTilesVert - yindex) * csteApplication.TILE_HEIGHT_IN_MAP);
            pGraphics.DrawImage(m_BackBuffer, destRect, srcRect, GraphicsUnit.Pixel);
        }

        public void PointToTile(int x, int y, ref int xindex, ref int yindex)
        {
            // unscale zoom;
            x = x / m_Zoom;
            y = y / m_Zoom;

            xindex = x / csteApplication.TILE_WIDTH_IN_MAP;
            yindex = y / csteApplication.TILE_HEIGHT_IN_MAP;
        }

        public void PointToBoundingRect(int x, int y, ref Rectangle bounding)
        {
            x = x / m_Zoom;
            y = y / m_Zoom;
            x = x / csteApplication.TILE_WIDTH_IN_MAP;
            y = y / csteApplication.TILE_HEIGHT_IN_MAP;
            bounding.Size = new Size((csteApplication.TILE_WIDTH_IN_MAP * m_Zoom) + 6, (csteApplication.TILE_HEIGHT_IN_MAP * m_Zoom) + 6);
            bounding.X = (x * csteApplication.TILE_WIDTH_IN_MAP * m_Zoom) - 3;
            bounding.Y = (y * csteApplication.TILE_HEIGHT_IN_MAP * m_Zoom) - 3;
        }

        public void PlotTile(int xindex, int yindex, int TileID)
        {
            if (xindex < 0 || yindex < 0 || yindex >= m_Height || xindex >= m_Width)
                return;
            m_Tiles[yindex, xindex] = TileID;
            m_TileLibrary.DrawTile(m_BackBufferDC, TileID, xindex * csteApplication.TILE_WIDTH_IN_MAP, yindex * csteApplication.TILE_HEIGHT_IN_MAP);

            foreach (var item in m_TileLibrary.ObjMonde)
            {
                string[] id = item.Key.Split(',').ToArray();
                int idTile = int.Parse(id[0]);

                if (idTile == TileID)
                {
                    m_currTile = item.Value;
                    return;
                }
            }
        }

        /// <summary>
        /// Description: G�re la sauvegarde d'un [Monde]
        /// D�tails: AjouterMonde() et SupprimerMonde() => MondeController
        /// </summary>
        /// <param name="strFilename"></param>
        /// <returns></returns>
        public int Save()
        {
            Monde monde = ctrl.GetMonde(currentMonde.Id);

            if (monde == null)
            {
                ctrl.AjouterMonde(currentMonde.Description, currentMonde.LimiteX, currentMonde.LimiteY);
            }
            else
            {
                if (monde.LimiteY != currentMonde.LimiteY || monde.LimiteX != currentMonde.LimiteX)
                    ctrl.ModifierDimensionsMonde(currentMonde.Id, currentMonde.LimiteX, currentMonde.LimiteY);

                if (monde.Description != currentMonde.Description)
                    ctrl.ModifierDescriptionMonde(currentMonde.Id, currentMonde.Description);
            }

            List<ObjetMonde> currObjs = ctrl.ListerObjetMondes(currentMonde);
            List<Monstre> currMonstres = ctrl.ListerMonstres(currentMonde);
            List<Item> currItems = ctrl.ListerItems(currentMonde);
            List<Hero> currHeroes = ctrl.ListerHeroes(currentMonde);

            if (currObjs != null && currObjs.Count != 0)
            {
                ctrl.ModifierMonde(currentMonde, currObjs);
            }

            if (currMonstres != null && currMonstres.Count != 0)
            {
                ctrl.ModifierMonde(currentMonde, currMonstres);
            }

            if (currItems != null && currItems.Count != 0)
            {
                ctrl.ModifierMonde(currentMonde, currItems);
            }

            if (currHeroes != null && currHeroes.Count != 0)
            {
                ctrl.ModifierMonde(currentMonde, currHeroes);
            }

            return 0;
        }

        /// <summary>
        /// Description: Faire afficher le [Monde] choisi dans la m�thode pr�c�dente, qui offrait la liste des mondes
        /// </summary>
        /// <param name="strFilename"></param>
        /// <returns></returns>
        public int Load(Monde m)
        {
            int width = -1;
            int height = -1;
            int data = -1;

            width = m.LimiteX;
            height = m.LimiteY;

            //if (width <= 0 || height <= 0 || data < 0 || m.Id < 0)
            if (width <= 0 || height <= 0 || m.Id < 0)
                return -1;
            if (width < 8 || width > csteApplication.MAP_MAX_WIDTH)
                return -1;
            if (height < 8 || height > csteApplication.MAP_MAX_HEIGHT)
                return -1;

            // Build Backbuffer
            m_Width = width;
            m_Height = height;

            m_Tiles = new int[m.LimiteY, m.LimiteX];
            m_TileLibrary = new CTileLibrary(m);

            //Checker avec vincent pour loader les tiles
            for (int i = 0; i < m_Height; i++)
            {
                for (int j = 0; j < m_Width; j++)
                {
                    m_Tiles[i, j] = m_TileLibrary.Tiles[i, j].imageId;
                }
            }

            m_BackBuffer = new Bitmap(m_Width * csteApplication.TILE_WIDTH_IN_MAP, m_Height * csteApplication.TILE_HEIGHT_IN_MAP);
            m_BackBufferDC = Graphics.FromImage(m_BackBuffer);

            currentMonde = m;

            Refresh();
            return 0;
        }

        /// <summary>
        /// Description: G�re chaque tuile d'une grandeur de 32 par 32
        /// D�tails: N'ajoute pas un [Monde]
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="defaulttile"></param>
        /// <returns></returns>
        public bool CreateNew(int width, int height, int defaulttile)
        {
            int i, j;

            if (width < 8 || width > csteApplication.MAP_MAX_WIDTH)
                return false;
            if (height < 8 || height > csteApplication.MAP_MAX_HEIGHT)
                return false;

            // Build Backbuffer
            m_Width = width;
            m_Height = height;
            int lastId = ctrl.ListerMondes().OrderByDescending(x => x.Id).Select(s => s.Id).First() + 1;
            currentMonde = new Monde()
            {
                Id = lastId,
                Description = "",
                LimiteX = m_Width,
                LimiteY = m_Height
            };
            m_TileLibrary = new CTileLibrary(currentMonde);

            try
            {
                m_Tiles = new int[m_Height, m_Width];

                for (i = 0; i < m_Height; i++)
                    for (j = 0; j < m_Width; j++)
                        m_Tiles[i, j] = defaulttile;
                m_BackBuffer = new Bitmap(m_Width * csteApplication.TILE_WIDTH_IN_MAP, m_Height * csteApplication.TILE_HEIGHT_IN_MAP);
                m_BackBufferDC = Graphics.FromImage(m_BackBuffer);

                Refresh();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}