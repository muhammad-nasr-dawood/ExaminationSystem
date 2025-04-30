using Azure.Core;
using ExaminationSystem.Core.IRepositories;
using ExaminationSystem.Core.Models;
using Newtonsoft.Json;
using System.Data;

namespace ExaminationSystem.EF.Repositories
{
    public class QuestionRepo : IQuestionRepo
    {

        private readonly ExaminationDBContext _dbContext;
        public QuestionRepo(ExaminationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddMCQuestion(long? staffId, byte? level, string content, int? topicId, byte? answerIndex, DataTable? images, DataTable answers)
        {
            return await _dbContext.Procedures.AddMCQuestionAsync(staffId, level, content, topicId, answerIndex, images, answers);
        }

        public async Task<int> AddTFQuestion(long? staffId, byte? level, string content, int? topicId, bool? isTrue, DataTable? images)
        {
            return await _dbContext.Procedures.AddTFQuestionsAsync(staffId, level, content, topicId, isTrue, images);
        }

        public async Task<List<GetQuestionsResult>> GetByTopic(int topicId, int order, byte type, byte level, int page, int limit)
        {
            return await _dbContext.Procedures.GetQuestionsAsync(topicId, order, type, level, page, limit);
        }


    }
}
