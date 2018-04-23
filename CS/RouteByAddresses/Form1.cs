using DevExpress.XtraMap;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ZoomToFitOnRouteCalculated {
    public partial class Form1 : Form {
        const String yourBingKey = "Insert Your Key Here";

        public Form1() {
            InitializeComponent();
            imageProvider.BingKey = yourBingKey;
        }

        private void Form1_Load(object sender, EventArgs e) {
            // Create a new route provider.
            BingRouteDataProvider provider = new BingRouteDataProvider { BingKey = yourBingKey };
            informationLayer.DataProvider = provider;

            // Send a request and handle request result.
            provider.RouteCalculated += OnRouteCalculated;
            // Note that waypoints are created using address instead of the location.
            provider.CalculateRoute(new List<RouteWaypoint>() {
                new RouteWaypoint("New York", "Belmont Park, New York, USA"),
                new RouteWaypoint("Las Vegas", "Lorenzi Park, Las Vegas, USA")});
            splashScreenManager1.ShowWaitForm();
        }

        void OnRouteCalculated(object sender, BingRouteCalculatedEventArgs e) {
            SearchBoundingBox box = e.CalculationResult.RouteResults[0].BoundingBox;
            GeoPoint topLeft = new GeoPoint {
                Latitude = box.NorthLatitude,
                Longitude = box.WestLongitude
            };
            GeoPoint bottomRight = new GeoPoint {
                Latitude = box.SouthLatitude,
                Longitude = box.EastLongitude
            };
            mapControl.ZoomToRegion(topLeft, bottomRight, 0.4);
            splashScreenManager1.CloseWaitForm();
        }
    }
}
