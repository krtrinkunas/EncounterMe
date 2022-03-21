using EncounterMeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EncounterMeApp.Services
{
    public interface ICaptureAttemptService
    {
        Task<IEnumerable<CaptureAttempt>> GetCaptureAttempts();
        Task<CaptureAttempt> GetCaptureAttempt(int id);
        Task AddCaptureAttempt(CaptureAttempt captureAttempt);
        Task UpdateCaptureAttempt(CaptureAttempt captureAttempt);
        Task DeleteCaptureAttempt(CaptureAttempt captureAttempt);
    }
}
