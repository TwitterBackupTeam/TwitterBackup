using System;
using System.ComponentModel.DataAnnotations;

namespace TwitterBackup.Web.Models.FavouriteTweetersViewModels
{
    public class TweetViewModel
    {
        public DateTime PostedOn { get; set; }

        [Required]
        [DataType(DataType.Text)]

        public string Content { get; set; }
    }
}
