﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApp_Web.Models;
using TaskApp_Web.Repositories;

namespace TaskApp_Web.Services
{
    public class InformationService : IInformationService
    {
        private readonly IInformationRepository _informationRepository;

        public InformationService(IInformationRepository informationRepository)
        {
            _informationRepository = informationRepository;
        }

        public async Task<IEnumerable<Information>> GetAllInformationsAsync()
        {
            return await _informationRepository.GetAllInformationsAsync();
        }

        public async Task<Information> GetInformationByIdAsync(int id)
        {
            return await _informationRepository.GetInformationByIdAsync(id);
        }

        public async Task<bool> AddInformationAsync(Information information)
        {
            return await _informationRepository.AddInformationAsync(information);
        }

        public async Task<bool> UpdateInformationAsync(Information information)
        {
            return await _informationRepository.UpdateInformationAsync(information);
        }

        public async Task<bool> DeleteInformationAsync(int id)
        {
            return await _informationRepository.DeleteInformationAsync(id);
        }
    }
}
