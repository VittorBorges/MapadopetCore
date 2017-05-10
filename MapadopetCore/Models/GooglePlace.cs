using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapadopetCore.Models
{
    public class GooglePlaceReturn
    {
        public string[] html_attributions { get; set; }
        public string status { get; set; }
        public List<GooglePlace> results { get; set; }


    }
    public class GooglePlace
    {
        public GoogleGeometry geometry { get; set; }
        public string icon { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public GoogleOpeningHours opening_hours { get; set; }
        public GooglePhotos[] photos { get; set; }
        public string place_id { get; set; }
        public decimal rating { get; set; }
        public string reference { get; set; }
        public string scope { get; set; }
        public string[] types { get; set; }
        public string vicinity { get; set; }
        private double[] _cord;
        public double[] cord
        {
            get
            {
                double[] d = new double[2] { geometry.location.lat, geometry.location.lng };
                return d;
            }
            set
            {
                double[] d = new double[2] { geometry.location.lat, geometry.location.lng };
                this._cord = d;
            }
        }
        public string img
        {
            get
            {
                var tamanho = photos != null ? photos.FirstOrDefault().width > 0 ? 300 : photos.FirstOrDefault().width : 0 ;
                return tamanho > 0 ? $"https://maps.googleapis.com/maps/api/place/photo?maxwidth={tamanho}&photoreference={photos.FirstOrDefault().photo_reference}&key=AIzaSyCfxAJhwYQ3NTgeQuxBCwaUyuuKCeHsNGI" : "";
            }
        }
    }

public class GooglePhotos
    {
        public int height { get; set; }
        public int width { get; set; }
        public string photo_reference { get; set; }
        public string[] html_attributions { get; set; }
    }

    public class GoogleOpeningHours
    {
        public bool open_now { get; set; }
        public string[] weekday_text { get; set; }
    }
}
