using ExaminationSystem.Core.IRepositories;
using ExaminationSystem.Core.Models;
using Newtonsoft.Json;

namespace ExaminationSystem.EF.Repositories
{
    public class QuestionRepo : IQuestionRepo
    {

        private readonly ExaminationDBContext _dbContext;
        public QuestionRepo(ExaminationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<List<GetQuestionsResult>> GetByTopic(int topicId, int order, byte type, byte level, int page, int limit)
        {
            return _dbContext.Procedures.GetQuestionsAsync(topicId, order, type, level, page, limit);
        }


    }
}
