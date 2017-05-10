namespace MapadopetCore.Models
{
    public class GoogleGeometry
    {
        public GoogleLocation location { get; set; }
        public GoogleViewport viewport { get; set; }
    }

    public class GoogleLocation
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class GoogleViewport
    {
        public GoogleLocation northeast { get; set; }
        public GoogleLocation southwest { get; set; }
    }
}