Imports DevExpress.XtraMap
Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace ZoomToFitOnRouteCalculated
    Partial Public Class Form1
        Inherits Form

        Private Const yourBingKey As String = "Insert Your Key Here"

        Public Sub New()
            InitializeComponent()
            imageProvider.BingKey = yourBingKey
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            ' Create a new route provider.
            Dim provider As BingRouteDataProvider = New BingRouteDataProvider With {.BingKey = yourBingKey}
            informationLayer.DataProvider = provider

            ' Send a request and handle request result.
            AddHandler provider.RouteCalculated, AddressOf OnRouteCalculated
            ' Note that waypoints are created using address instead of the location.
            provider.CalculateRoute(New List(Of RouteWaypoint)() From { _
                New RouteWaypoint("New York", "Belmont Park, New York, USA"), _
                New RouteWaypoint("Las Vegas", "Lorenzi Park, Las Vegas, USA") _
            })
            splashScreenManager1.ShowWaitForm()
        End Sub

        Private Sub OnRouteCalculated(ByVal sender As Object, ByVal e As BingRouteCalculatedEventArgs)
            Dim box As SearchBoundingBox = e.CalculationResult.RouteResults(0).BoundingBox
            Dim topLeft As GeoPoint = New GeoPoint With {.Latitude = box.NorthLatitude, .Longitude = box.WestLongitude}
            Dim bottomRight As GeoPoint = New GeoPoint With {.Latitude = box.SouthLatitude, .Longitude = box.EastLongitude}
            mapControl.ZoomToRegion(topLeft, bottomRight, 0.4)
            splashScreenManager1.CloseWaitForm()
        End Sub
    End Class
End Namespace
