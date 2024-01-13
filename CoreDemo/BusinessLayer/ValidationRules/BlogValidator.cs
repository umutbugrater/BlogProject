using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class BlogValidator : AbstractValidator<Blog>
    {
        public BlogValidator()
        {
            RuleFor(x => x.BlogTitle).NotEmpty().WithMessage("Blog başlığını boş geçemezsiniz")
                //.Length(5, 150).WithMessage("Lütfen 5 ile 150 karakter arasında veri girişi yapın.")
                .MaximumLength(150).WithMessage("Lütfen 150 karakterden daha az veri girişi yapın")
                .MinimumLength(5).WithMessage("Lütfen 5 karakterden daha fazla veri girişi yapın");
            RuleFor(x => x.BlogContent).NotEmpty().WithMessage("Blog içeriğini boş geçemezsiniz").MinimumLength(130).WithMessage("130 karakterden az içerik yazamazsınız");
            RuleFor(x => x.BlogImage).NotEmpty().WithMessage("Blog görselini boş geçemezsiniz");
            //RuleFor(x => x.BlogThumbnailImage).NotEmpty().WithMessage("Blog THUMBNAİL ALANIIN boş geçemezsiniz");
            
        }
    }
}
