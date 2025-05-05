using ExaminationSystem.Core.IRepositories;
using ExaminationSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSystem.EF.Repositories
{
    public class TopicRepo : ITopics
    {
        private readonly ExaminationDBContext _dbContext;
        public TopicRepo(ExaminationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<GetTopicsByCourseResult>> GetTopicsByCourse(int courseId, int? pageSize, int? pageNumber)
        {
            return await _dbContext.Procedures.GetTopicsByCourseAsync(courseId, pageSize, pageNumber);
        }


    }
        
}
