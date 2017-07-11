using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace StaplesTask.Models
{
    public class FormViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Sex")]
        public string Sex { get; set; }

        [Display(Name = "Birth Day")]
        public int[] BirthDay { get; set; }

        [Display(Name = "Birth Month")]
        public List<SelectListItem> BirthMonth { get; set; }
        public string SelectedMonth { get; set; }

        [Display(Name = "Birth Year")]
        public int[] BirthYear { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Short Description")]
        public string Description { get; set; }

        public FormViewModel()
        {
            int YearsRangeStart = 1900;
            BirthDay = Enumerable.Range(1, 31).ToArray();
            BirthYear = Enumerable.Range(YearsRangeStart, DateTime.Today.Year - (YearsRangeStart - 1)).Reverse().ToArray();
            BirthMonth = new List<SelectListItem>();
            for (int i = 0; i < 12; i++)
            {
                BirthMonth.Add(new SelectListItem
                {
                    Text = DateTimeFormatInfo.InvariantInfo.MonthNames[i],
                    Value = (i + 1).ToString()
                });
            }
        }
    }

    public class InfoLabelViewModel
    {
        public string InfoMessage { get; set; }
        public string LabelClass { get; set; }

        public InfoLabelViewModel(bool detailsSaved)
        {
            InfoMessage = detailsSaved ? "Details have been saved." : "This person is already saved.";
            LabelClass = detailsSaved ? "alert-success" : "alert-danger";
        }
    }
}