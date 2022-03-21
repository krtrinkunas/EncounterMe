using EncounterMeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface ICaptureAttemptRepository
    {
        Task<IEnumerable<CaptureAttempt>> Get();
        Task<CaptureAttempt> Get(int id);
        Task<CaptureAttempt> Create(CaptureAttempt captureAttempt);
        Task Update(CaptureAttempt captureAttempt);
        Task Delete(int id);

    }
}
