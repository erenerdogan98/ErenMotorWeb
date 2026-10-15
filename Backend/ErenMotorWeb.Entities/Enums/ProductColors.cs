using System.ComponentModel.DataAnnotations;

namespace ErenMotorWeb.Entities.Enums
{
    public enum ProductColors
    {
        [Display(Name = "Kırmızı")]
        Red = 1,

        [Display(Name = "Sarı")]
        Yellow = 2,

        [Display(Name = "Mavi")]
        Blue = 3,

        [Display(Name = "Yeşil")]
        Green = 4,

        [Display(Name = "Siyah")]
        Black = 5,

        [Display(Name = "Beyaz")]
        White = 6,

        [Display(Name = "Gri")]
        Gray = 7,

        [Display(Name = "Turuncu")]
        Orange = 8,

        [Display(Name = "Mor")]
        Purple = 9,

        [Display(Name = "Pembe")]
        Pink = 10,

        [Display(Name = "Kahverengi")]
        Brown = 11,

        [Display(Name = "Lacivert")]
        NavyBlue = 12,

        [Display(Name = "Bej")]
        Beige = 13,

        [Display(Name = "Gümüş")]
        Silver = 14,

        [Display(Name = "Altın")]
        Gold = 15,

        [Display(Name = "Şeffaf")]
        Transparent = 16,

        [Display(Name = "Turkuaz")]
        Turquoise = 17
    }
}