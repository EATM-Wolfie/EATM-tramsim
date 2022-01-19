using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RouteBuilder
{
	//Link to the place I learned about the map usage.
	//http://www.independent-software.com/gmap-net-beginners-tutorial-adding-clickable-markers-to-your-map-updates-for-vs2015-and-gmap-1-7.html

	public partial class FrmMap : Form
	{
		private GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");

		public FrmMap()
		{
			InitializeComponent();
		}

		private void FrmMap_Load(object sender, EventArgs e)
		{
			gmap.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
			GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
			gmap.Position = new GMap.NET.PointLatLng(51.5291448, -0.057708);

			//gmap.SetPositionByKeywords("Paris, France");
			gmap.ShowCenter = false;
			gmap.ShowCenter = false;
			gmap.MinZoom = 4;                                                                            // whole world zoom
			gmap.MaxZoom = 50;
			gmap.Zoom = 18;
			gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionWithoutCenter;                // lets the map use the mousewheel to zoom
			gmap.CanDragMap = true;                                                                      // lets the user drag the map
			gmap.DragButton = MouseButtons.Left;                                                          // lets the user drag the map with the left mouse button
			gmap.IgnoreMarkerOnMouseWheel = true;

			gmap.OnMapClick += Gmap_OnMapClick;
			gmap.OnMarkerClick += Gmap_OnMarkerClick;

			// = new GMap.NET.WindowsForms.GMapOverlay("markers");
			GMap.NET.WindowsForms.GMapMarker marker =
				new GMap.NET.WindowsForms.Markers.GMarkerGoogle(
					new GMap.NET.PointLatLng(51.5291448, -0.057708),
					GMap.NET.WindowsForms.Markers.GMarkerGoogleType.blue_pushpin);
			markers.Markers.Add(marker);
			gmap.Overlays.Add(markers);
			GMapOverlay routes = new GMapOverlay("routes");
			gmap.Overlays.Add(routes);
		}

		private void Gmap_OnMarkerClick(GMap.NET.WindowsForms.GMapMarker item, MouseEventArgs e)
		{
			Console.WriteLine(String.Format("Marker {0} was clicked.", item.Tag));
		}

		private void Gmap_OnMapClick(PointLatLng pointClick, MouseEventArgs e)
		{
			GMap.NET.WindowsForms.GMapMarker marker =
			   new GMap.NET.WindowsForms.Markers.GMarkerGoogle(pointClick,

				   GMap.NET.WindowsForms.Markers.GMarkerGoogleType.blue_pushpin);
			marker.ToolTipText = "hello\nout there";
			marker.ToolTip.Fill = Brushes.Black;
			marker.ToolTip.Foreground = Brushes.White;
			marker.ToolTip.Stroke = Pens.Black;
			marker.ToolTip.TextPadding = new Size(20, 20);
			markers.Markers.Add(marker);
			//LoadPolygons();
			//LoadRoute();
			AddRouteEntry(pointClick);
			//MessageBox.Show(String.Format("Lat = {0} Long = {1}", pointClick.Lat, pointClick.Lng));
		}

		private void LoadPolygons()
		{
			GMapOverlay polygons = new GMapOverlay("polygons");
			List<PointLatLng> points = new List<PointLatLng>();
			points.Add(new PointLatLng(48.866383, 2.323575));
			points.Add(new PointLatLng(48.863868, 2.321554));
			points.Add(new PointLatLng(48.861017, 2.330030));
			points.Add(new PointLatLng(48.863727, 2.331918));
			GMapPolygon polygon = new GMapPolygon(points, "Jardin des Tuileries");
			polygons.Polygons.Add(polygon);
			gmap.Overlays.Add(polygons);
		}

		private void LoadRoute()
		{
			GMapOverlay routes = new GMapOverlay("routes");
			List<PointLatLng> points = new List<PointLatLng>();
			points.Add(new PointLatLng(48.866383, 2.323575));
			points.Add(new PointLatLng(48.863868, 2.321554));
			points.Add(new PointLatLng(48.861017, 2.330030));
			GMapRoute route = new GMapRoute(points, "A walk in the park");
			route.Stroke = new Pen(Color.Red, 3);
			routes.Routes.Add(route);
			gmap.Overlays.Add(routes);
		}

		private void AddRouteEntry(PointLatLng p)
		{
			lb.Items.Clear();
			foreach (GMapOverlay overlay in gmap.Overlays)
			{
				if (overlay.Id == "routes")
				{
					foreach (GMapRoute gMapRoute in overlay.Routes)
					{
						gMapRoute.Points.Add(p);
						foreach (PointLatLng pnt in gMapRoute.Points)
						{
							lb.Items.Add(string.Format("Point {0},{1}", pnt.Lat, pnt.Lng));
						}
					}
				}
			}
		}
	}
}
