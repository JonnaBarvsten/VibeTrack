using VibeTrack.Application.DTOs.MoodLogs;
using VibeTrack.Application.Interfaces;
using VibeTrack.Domain.Entities;
using VibeTrack.Domain.Interfaces;

namespace VibeTrack.Application.Services
{
    public class MoodLogService : IMoodLogService 
    {
        private readonly IMoodLogRepository _moodLogRepository;
        public MoodLogService(IMoodLogRepository moodLogRepository)
        {
            _moodLogRepository = moodLogRepository;
        }

        public async Task<MoodLogDto?> AddMoodLogAsync(CreateMoodLogDto createMoodLogDto, int userId)
        {
            var isValid = await _moodLogRepository.IsDailyStatOwnedByUserAsync(createMoodLogDto.DailyStatsId, userId);

            if (!isValid)
            {
                return null;
            }

            var newMoodLog = new MoodLog
            {
                DailyStatsId = createMoodLogDto.DailyStatsId,
                EnergyLevelId = createMoodLogDto.EnergyLevel,
                MoodId = createMoodLogDto.Mood, 
                StressLevelId = createMoodLogDto.StressLevel
            };

            await _moodLogRepository.AddAsync(newMoodLog);

            var moodLogDto = new MoodLogDto
            {
                Id = newMoodLog.Id,
                LoggedAt = newMoodLog.LoggedAt,
                DailyStatsId = newMoodLog.DailyStatsId,
                Mood = newMoodLog.MoodId,
                EnergyLevel = newMoodLog.EnergyLevelId,
                StressLevel = newMoodLog.StressLevelId
            };

            return moodLogDto;
        }

        public async Task<bool> DeleteMoodLogAsync(int id, int userId)
        {
            var moodLog = await _moodLogRepository.GetByIdAsync(id);

            if(moodLog == null)
            {
                return false;
            }

            var isValid = await _moodLogRepository.IsDailyStatOwnedByUserAsync(moodLog.DailyStatsId, userId);

            if (!isValid)
            {
                return false;
            }

            await _moodLogRepository.DeleteAsync(moodLog.Id);

            return true; 
        }

        public async Task<List<MoodLogDto>> GetAllMoodLogsAsync(int userId)
        {
            var moodLogs = await _moodLogRepository.GetAllAsync();

            var moodLogDto = moodLogs.Where(ml => ml.DailyStats != null && ml.DailyStats.DailyLog != null && ml.DailyStats.DailyLog.UserId == userId)
                .Select(ml => new MoodLogDto
            {
                    Id = ml.Id,
                    LoggedAt = ml.LoggedAt,
                    DailyStatsId = ml.DailyStatsId,
                    Mood = ml.MoodId,
                    MoodName = ml.Mood.Name,
                    EnergyLevel = ml.EnergyLevelId,
                    StressLevel = ml.StressLevelId
            })
                .ToList();

            return moodLogDto;
        }

        public async Task<MoodLogDto?> GetMoodLogByIdAsync(int id, int userId)
        {
            var moodLog = await _moodLogRepository.GetByIdAsync(id);

            if(moodLog == null)
            {
                return null;
            }

            var isValid = await _moodLogRepository.IsDailyStatOwnedByUserAsync(moodLog.DailyStatsId, userId);

            if (!isValid)
            {
                return null; 
            }

            var moodLogDto = new MoodLogDto
            {
                
                Id = moodLog.Id,
                LoggedAt = moodLog.LoggedAt,
                DailyStatsId = moodLog.DailyStatsId,
                Mood = moodLog.MoodId,
                EnergyLevel = moodLog.EnergyLevelId,
                StressLevel = moodLog.StressLevelId
            };

            return moodLogDto;
        }

        public async Task<MoodLogDto?> UpdateMoodLogAsync(int id, UpdateMoodLogDto updateMoodLogDto, int userId)
        {
            var moodLog = await _moodLogRepository.GetByIdAsync(id);

            if(moodLog == null)
            {
                return null;
            }

            var isValid = await _moodLogRepository.IsDailyStatOwnedByUserAsync(moodLog.DailyStatsId, userId);

            if (!isValid)
            {
                return null; 
            }

            moodLog.MoodId = updateMoodLogDto.Mood;
            moodLog.EnergyLevelId = updateMoodLogDto.EnergyLevel;
            moodLog.StressLevelId = updateMoodLogDto.StressLevel;

            await _moodLogRepository.UpdateAsync(moodLog);

            var moodLogDto = new MoodLogDto
            {
                Id = moodLog.Id,
                LoggedAt = moodLog.LoggedAt,
                DailyStatsId = moodLog.DailyStatsId,
                Mood = moodLog.MoodId,
                EnergyLevel = moodLog.EnergyLevelId,
                StressLevel = moodLog.StressLevelId
            };

            return moodLogDto;
        }

        public async Task<List<Mood>> GetAllMoodsAsync()
        {
            return await _moodLogRepository.GetAllMoodsAsync();
        }
    }
}
