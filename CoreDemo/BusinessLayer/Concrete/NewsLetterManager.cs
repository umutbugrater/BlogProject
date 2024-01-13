using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class NewsLetterManager : INewsLetterService
    {
        INewsletterDal _newsletterdal;

        public NewsLetterManager(INewsletterDal newsletterdal)
        {
            _newsletterdal = newsletterdal;
        }

        public void AddNewsLetter(NewsLetter newsLetter)
        {
            _newsletterdal.Insert(newsLetter);
        }
    }
}
