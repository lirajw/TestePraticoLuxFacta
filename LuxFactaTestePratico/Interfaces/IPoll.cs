using LuxFactaTestePratico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuxFactaTestePratico.Interfaces
{
    public interface IPoll : IDisposable
    {
        PollReturn InsertPoll(PollInsert poll);
        Stats SelectStats(int Poll_Id);
        Poll SelectPoll(int Poll_Id);
    }
}