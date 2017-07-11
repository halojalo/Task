using System.Web.Mvc;
using StaplesTask.Models;
using AutoMapper;
using PersonLibrary;

namespace StaplesTask.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new FormViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ViewResult SaveDetails(FormViewModel form)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FormViewModel, Person>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src));
                cfg.CreateMap<FormViewModel, BirthDate>()
                .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.BirthDay[0]))
                .ForMember(dest => dest.Month, opt => opt.MapFrom(src => int.Parse(src.SelectedMonth)))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.BirthYear[0]));
            });
            var mapper = config.CreateMapper();
            var person = mapper.Map<FormViewModel, Person>(form);
            bool[] saved = { person.SaveToDatabase(), person.SaveFile("txt"), person.SaveFile("xml") };
            var model = new InfoLabelViewModel(saved[0] || saved[1] || saved[2]);

            return View("InfoLabel", model);
        }
    }
}