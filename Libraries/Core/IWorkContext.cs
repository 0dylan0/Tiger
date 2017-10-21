using Core.Domain;
using Core.Domain.Common;
using Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IWorkContext
    {
        Core.Domain.Common.Users CurrentUser { get; set; }

        //Hotel CurrentHotel { get; set; }

        //Language CurrentLanguage { get; set; }

        Guid CurrentRequestId { get; }

        string CurrentUserCode { get; }

        bool IsAlreadyLogin();
    }
}
