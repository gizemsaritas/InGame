using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InGame.Business.Concrete.DTO.Concrete;

namespace InGame.Business.Interface
{
    public interface IMailService
    {
        Task<ServiceResult> SendResetPasswordMailAsync(string url, string mail);
    }
}
