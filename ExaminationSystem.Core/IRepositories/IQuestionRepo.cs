using ExaminationSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem.Core.IRepositories
{
    public interface IQuestionRepo
    {
        public Task<List<GetQuestionsResult>> GetByTopic(int topicId, int order, byte type, byte level, int page, int limit);

        public Task<int> AddTFQuestion(long? staffId, byte? level, string content, int? topicId, bool? isTrue, DataTable? images);

        public Task<int> AddMCQuestion(long? staffId, byte? level, string content, int? topicId, byte? answerIndex, DataTable? images, DataTable answers);


    }
}
