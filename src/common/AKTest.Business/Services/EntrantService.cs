using AKTest.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AKTest.Data;
using System.Linq;
using AKTest.Common;

namespace AKTest.Business.Services
{
    public class EntrantService : IEntrantService
    {
        private readonly IEntrantRepository _entrantsRepository;
        private IMapper _mapper;

        public EntrantService(IEntrantRepository entrantsRepository)
        {
            this._entrantsRepository = entrantsRepository;

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EntrantEntity, Entrants>();
            });
            _mapper = config.CreateMapper();
        }
        public async Task<List<Entrants>> GetEntrantAll()
        {
            var entrantEntityList = await _entrantsRepository.GetEntrantRepository();

            if (!entrantEntityList.Any())
               throw new Exception(Constants.ErrorMessage_Result_NotFound);

            return _mapper.Map<List<Entrants>>(entrantEntityList);
        }

        public async Task<Entrants> GetEntrantById(int? id)
        {
            if (id < 1 || id==null)
                throw new Exception(Constants.ErrorMessage_ID_NotFound);

            var entrantEntityList = await _entrantsRepository.GetEntrantRepository();

            if (!entrantEntityList.Any())
                throw new Exception(Constants.ErrorMessage_Result_NotFound);

            var entrant = entrantEntityList.Where(x => x.id.Equals(id)).FirstOrDefault();

            return _mapper.Map<Entrants>(entrant);
        }

        public async Task PostEntrant(Entrants entrants)
        {
            if (entrants == null)
                throw new Exception(Constants.ErrorMessage_BadRequest);

            if (entrants.id == null || entrants.id < 1)
                throw new Exception(Constants.ErrorMessage_ID_NotFound);

            if (string.IsNullOrWhiteSpace(entrants.firstName))
                throw new Exception(Constants.ErrorMessage_FirstName_NotFound);

            if (string.IsNullOrWhiteSpace(entrants.lastName))
                throw new Exception(Constants.ErrorMessage_LastName_NotFound);

            var entrantEntityList = await _entrantsRepository.GetEntrantRepository();

            if (!entrantEntityList.Any())
                throw new Exception(Constants.ErrorMessage_Result_NotFound);

            var postEntrant = _mapper.Map<List<Entrants>>(entrantEntityList);

            postEntrant.Add(entrants);


        }
        public async Task DeleteEntrant(int? id)
        {
            if (id < 1 || id == null)
                throw new Exception(Constants.ErrorMessage_ID_NotFound);

            var entrantEntityList = await _entrantsRepository.GetEntrantRepository();

            if (!entrantEntityList.Any())
                throw new Exception(Constants.ErrorMessage_Result_NotFound);

            var deleteEntrant = _mapper.Map<List<Entrants>>(entrantEntityList);

            var entrant = _mapper.Map<Entrants>(entrantEntityList.Where(x => x.id.Equals(id)).FirstOrDefault());


            deleteEntrant.Remove(entrant);
        }
    }
}
