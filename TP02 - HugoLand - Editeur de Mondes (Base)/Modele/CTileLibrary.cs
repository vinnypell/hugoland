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
    /// Summary description for CTileLibrary.
    /// </summary>
    public class CTileLibrary
    {
        private int m_Count;			// number of tiles
        private Bitmap m_TileSource;		// to be loaded from external File or resource...
        private int m_Width;
        private int m_Height;
        private MondeController ctrl = new MondeController();
        private ObjetMondeController objCtrl = new ObjetMondeController();

        public List<ObjetMonde> objetMondes = new List<ObjetMonde>();
        public List<Monstre> monstres = new List<Monstre>();
        public List<Item> items = new List<Item>();

        private Tile m_clickedTile;
        public Tile clickedTile
        {
            get
            {
                return m_clickedTile;
            }
            set
            {
                m_clickedTile = value;
            }
        }

        private List<int> m_TileID = new List<int>();
        public List<int> TileID
        {
            get
            {
                return m_TileID;
            }
            set
            {
                m_TileID = value;
            }
        }

        // Contient l'image � afficher
        private Tile[,] m_Tiles;
        public Tile[,] Tiles
        {
            get
            {
                return m_Tiles;
            }
            set
            {
                m_Tiles = value;
            }
        }

        private Dictionary<string, Tile> _ObjMonde = new Dictionary<string, Tile>();
        public Dictionary<string, Tile> ObjMonde
        {
            get { return _ObjMonde; }
            set { _ObjMonde = value; }
        }

        // Count
        public int Count
        {
            get
            {
                return m_Count;
            }
            set
            {
                m_Count = value;
            }
        }

        //Width
        public int Width
        {
            get
            {
                return m_Width; //m_TileSource.Width;;
            }
        }

        //Height
        public int Height
        {
            get
            {
                return m_Height; //m_TileSource.Height;;
            }
        }

        public CTileLibrary()
        {
            m_TileSource = new Bitmap(@"gamedata\AllTiles.bmp");
            m_Width = (m_TileSource.Width / csteApplication.TILE_WIDTH_IN_IMAGE) + 1;
            m_Height = (m_TileSource.Height / csteApplication.TILE_HEIGHT_IN_IMAGE) + 1;
            Monde newMonde = null;
            readTileDefinitions(newMonde);
        }

        public CTileLibrary(Monde m)
        {
            m_TileSource = new Bitmap(@"gamedata\AllTiles.bmp");
            m_Width = (m_TileSource.Width / csteApplication.TILE_WIDTH_IN_IMAGE) + 1;
            m_Height = (m_TileSource.Height / csteApplication.TILE_HEIGHT_IN_IMAGE) + 1;
            readTileDefinitions(m);
        }

        public void Draw(Graphics pGraphics, Rectangle destRect)
        {
            Rectangle srcRect = new Rectangle(0, 0, (m_Width - 1) * csteApplication.TILE_WIDTH_IN_IMAGE, (m_Height - 1) * csteApplication.TILE_HEIGHT_IN_IMAGE);
            Rectangle destRect2 = new Rectangle(0, 0, (m_Width - 1) * csteApplication.TILE_WIDTH_IN_IMAGE, (m_Height - 1) * csteApplication.TILE_HEIGHT_IN_IMAGE);
            pGraphics.DrawImage(m_TileSource, destRect2, srcRect, GraphicsUnit.Pixel);
        }

        public void DrawTile(Graphics pGraphics, int ID, int X, int Y)
        {
            Rectangle sourcerect = new Rectangle((ID % csteApplication.TILE_WIDTH_IN_LIBRARY) * csteApplication.TILE_WIDTH_IN_IMAGE, (ID / csteApplication.TILE_HEIGHT_IN_LIBRARY) * csteApplication.TILE_HEIGHT_IN_IMAGE, csteApplication.TILE_WIDTH_IN_IMAGE, csteApplication.TILE_HEIGHT_IN_IMAGE);

            Rectangle destrect = new Rectangle(X, Y, csteApplication.TILE_WIDTH_IN_MAP, csteApplication.TILE_HEIGHT_IN_MAP);
            pGraphics.DrawImage(m_TileSource, destrect, sourcerect, GraphicsUnit.Pixel);
        }

        public void DrawTile(Graphics pGraphics, int ID, Rectangle destrect)
        {
            Rectangle sourcerect = new Rectangle((ID % csteApplication.TILE_WIDTH_IN_LIBRARY) * csteApplication.TILE_WIDTH_IN_IMAGE, (ID / csteApplication.TILE_HEIGHT_IN_LIBRARY) * csteApplication.TILE_HEIGHT_IN_IMAGE, csteApplication.TILE_WIDTH_IN_IMAGE, csteApplication.TILE_HEIGHT_IN_IMAGE);
            pGraphics.DrawImage(m_TileSource, destrect, sourcerect, GraphicsUnit.Pixel);
        }

        public void GetSourceRect(Rectangle sourcerect, int ID)
        {
            sourcerect.X = ID % csteApplication.TILE_WIDTH_IN_LIBRARY;
            sourcerect.Y = ID / csteApplication.TILE_HEIGHT_IN_LIBRARY;
            sourcerect.Width = csteApplication.TILE_WIDTH_IN_IMAGE;
            sourcerect.Height = csteApplication.TILE_HEIGHT_IN_IMAGE;
        }

        public int TileToTileID(int xindex, int yindex)
        {
            if (xindex > m_Width)
                xindex = m_Width;
            if (yindex > m_Height)
                yindex = m_Height;
            return (yindex * 32 + xindex);
        }

        public void PointToBoundingRect(int x, int y, ref Rectangle bounding)
        {
            x = x / csteApplication.TILE_WIDTH_IN_IMAGE;
            y = y / csteApplication.TILE_HEIGHT_IN_IMAGE;
            bounding.Size = new Size(csteApplication.TILE_WIDTH_IN_IMAGE + 6, csteApplication.TILE_HEIGHT_IN_IMAGE + 6);
            bounding.X = (x * csteApplication.TILE_WIDTH_IN_IMAGE) - 3;
            bounding.Y = (y * csteApplication.TILE_HEIGHT_IN_IMAGE) - 3;
        }

        public void PointToTile(int x, int y, ref int xindex, ref int yindex)
        {
            xindex = x / csteApplication.TILE_WIDTH_IN_IMAGE;
            yindex = y / csteApplication.TILE_HEIGHT_IN_IMAGE;
        }

        private void TilesToObjects(Tile tile, Monde monde)
        {
            int imageId = TileToTileID(tile.X_Image, tile.Y_Image);
            m_TileID.Add(imageId);

            switch (tile.TypeObjet)
            {
                case TypeTile.ObjetMonde:
                    objetMondes.Add(new ObjetMonde
                    {
                        MondeId = monde.Id,
                        Description = tile.Name,
                        x = tile.X_Image,
                        y = tile.Y_Image,
                        TypeObjet = (int)tile.TypeObjet,
                        ImageId = imageId
                    });
                    break;
                case TypeTile.Monstre:
                    monstres.Add(new Monstre
                    {
                        Monde = monde,
                        x = tile.X_Image,
                        y = tile.Y_Image,
                        Nom = tile.Name,
                        ImageId = imageId
                    });
                    break;
                case TypeTile.Item:
                    items.Add(new Item
                    {
                        Nom = tile.Name,
                        Description = tile.Category,
                        x = tile.X_Image,
                        y = tile.Y_Image,
                        ImageId = imageId,
                        MondeId = monde.Id
                    });
                    break;
                default:
                    break;
            }
        }

        public void readTileDefinitions(Monde m)
        {
            if (m == null)
            {
                int lastId = ctrl.ListerMondes().OrderByDescending(x => x.Id).Select(s => s.Id).First() + 1;
                m = new Monde()
                {
                    Id = lastId,
                    Description = "",
                    LimiteX = 32,
                    LimiteY = 32
                };
            }

            using (StreamReader stream = new StreamReader(@"gamedata\AllTilesLookups.csv"))
            {
                string line;

                while ((line = stream.ReadLine()) != null)
                {
                    //separate out the elements of the
                    string[] elements = line.Split(',');

                    Tile objMonde;
                    objMonde = new Tile(elements);
                    TilesToObjects(objMonde, m);
                    _ObjMonde.Add(objMonde.Name, objMonde);
                }
            }

            ObjetMonde tuileDefaut = objCtrl.GetObjetMondeDefault();
            if (tuileDefaut == null)
            {
                tuileDefaut = objetMondes.FirstOrDefault(x => x.ImageId == 32);
            }

            Tile defaultTile = new Tile()
            {
                Name = tuileDefaut.Description,
                Bitmap = m_TileSource,
                X_Image = tuileDefaut.x,
                Y_Image = tuileDefaut.y,
                TypeObjet = TypeTile.ObjetMonde,
                IndexTypeObjet = (int)TypeTile.ObjetMonde,
                imageId = (int)tuileDefaut.ImageId
            };

            Tiles = new Tile[m.LimiteY, m.LimiteX];

            if (m.ObjetMondes.Count != 0 && m.ObjetMondes != null ||
                m.Monstres.Count != 0 && m.Monstres != null ||
                m.Items.Count != 0 && m.Items != null)
            {
                for (int y = 0; y < m.LimiteY; y++)
                {
                    for (int x = 0; x < m.LimiteX; x++)
                    {
                        // �a plante pour une raison que j'ignore
                        List<Item> items = ctrl.ListerItems(m).Where(i => i.x == x && i.y == y).ToList();
                        ObjetMonde objets = ctrl.ListerObjetMondes(m).FirstOrDefault(o => o.x == x && o.y == y);
                        Monstre monstres = ctrl.ListerMonstres(m).FirstOrDefault(o => o.x == x && o.y == y);

                        // H�ro n'a pas d'imageID dans la table, alors je me doute qu'on enregistre la position du joueur
                        // dans la map, si on ne le fait pas, ce sera ici de le faire et d'ajouter une propri�t� � cet effet
                        //Hero heroes = ctrl.ListerHeroes(m).FirstOrDefault(o => o.x == x && o.y == y);

                        // Seul les items peuvent �tre stacker
                        if (items.Count > 0 && items != null)
                        {
                            //if (items.Count > 1)
                            //{
                            //    List<Tile> tiles = new List<Tile>();
                            //    foreach (Item item in items)
                            //    {
                            //        tiles.Add(new Tile()
                            //        {
                            //            Name = item.Nom,
                            //            Bitmap = m_TileSource,
                            //            X_Image = (int)item.x,
                            //            Y_Image = (int)item.y,
                            //            TypeObjet = TypeTile.Item,
                            //            IndexTypeObjet = (int)TypeTile.Item,
                            //            Rectangle = new Rectangle((int)item.x - 1 * Tile.TileSizeX,
                            //                                      (int)item.y - 1 * Tile.TileSizeY,
                            //                                      Tile.TileSizeX * 1, Tile.TileSizeY)
                            //        });
                            //    }
                            //    //Tiles[y, x] = tiles;
                            //}
                            //else
                            //{
                            //}

                            Item item = items.FirstOrDefault();
                            Tiles[y, x] = new Tile()
                            {
                                Name = item.Nom,
                                Bitmap = m_TileSource,
                                X_Image = (int)item.x,
                                Y_Image = (int)item.y,
                                TypeObjet = TypeTile.Item,
                                IndexTypeObjet = (int)TypeTile.Item,
                                Rectangle = new Rectangle((int)item.x - 1 * Tile.TileSizeX,
                                                              (int)item.y - 1 * Tile.TileSizeY,
                                                              Tile.TileSizeX * 1, Tile.TileSizeY),
                                imageId = (int)item.ImageId
                            };
                        }
                        else if (objets != null)
                        {
                            Tiles[y, x] = new Tile()
                            {
                                Name = objets.Description,
                                Bitmap = m_TileSource,
                                X_Image = objets.x,
                                Y_Image = objets.y,
                                TypeObjet = TypeTile.ObjetMonde,
                                IndexTypeObjet = (int)TypeTile.ObjetMonde,
                                Rectangle = new Rectangle((int)objets.x - 1 * Tile.TileSizeX,
                                                                  (int)objets.y - 1 * Tile.TileSizeY,
                                                                  Tile.TileSizeX * 1, Tile.TileSizeY),
                                imageId = (int)objets?.ImageId
                            };
                        }
                        else if (monstres != null)
                        {
                            Tiles[y, x] = new Tile()
                            {
                                Name = monstres.Nom,
                                Bitmap = m_TileSource,
                                X_Image = monstres.x,
                                Y_Image = monstres.y,
                                TypeObjet = TypeTile.ObjetMonde,
                                IndexTypeObjet = (int)TypeTile.ObjetMonde,
                                Rectangle = new Rectangle((int)monstres.x - 1 * Tile.TileSizeX,
                                                                  (int)monstres.y - 1 * Tile.TileSizeY,
                                                                  Tile.TileSizeX * 1, Tile.TileSizeY),
                                imageId = (int)monstres.ImageId
                            };
                        }
                        else
                        {
                            m.ObjetMondes.Add(tuileDefaut);
                            tuileDefaut.x = x;
                            tuileDefaut.y = y;
                            tuileDefaut.Id++;
                            defaultTile.X_Image = x;
                            defaultTile.Y_Image = y;

                            Tiles[y, x] = defaultTile;
                        }
                        //else if (heroes != null)
                        //{
                        //    Tiles[y, x] = new Tile()
                        //    {
                        //        Name = heroes.NomHero,
                        //        Bitmap = m_TileSource,
                        //        X_Image = heroes.x,
                        //        Y_Image = heroes.y,
                        //        TypeObjet = TypeTile.ObjetMonde,
                        //        IndexTypeObjet = (int)TypeTile.ObjetMonde,
                        //        Rectangle = new Rectangle((int)heroes.x - 1 * Tile.TileSizeX,
                        //                                          (int)heroes.y - 1 * Tile.TileSizeY,
                        //                                          Tile.TileSizeX * 1, Tile.TileSizeY)
                        //    };
                        //}
                    }
                }
            }
            else
            {
                for (int y = 0; y < m.LimiteY; y++)
                {
                    for (int x = 0; x < m.LimiteX; x++)
                    {
                        tuileDefaut.x = x;
                        tuileDefaut.y = y;
                        tuileDefaut.Id++;
                        m.ObjetMondes.Add(tuileDefaut);

                        defaultTile.X_Image = x;
                        defaultTile.Y_Image = y;
                        Tiles[y, x] = defaultTile;
                    }
                }
            }
        }
    }
}


