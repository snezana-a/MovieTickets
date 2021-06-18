using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MT.Services.Interface
{
    public interface IBackgroundEmailSender
    {
        Task DoWork();
    }
}
