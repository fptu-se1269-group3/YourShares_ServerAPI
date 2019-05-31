﻿using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YourShares.Data.Interfaces;
using YourShares.Domain.ApiResponse;
using YourShares.Domain.Models;

namespace YourShares.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepository<Company> _companyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyService(IUnitOfWork UnitOfWork, IRepository<Company> CompanyRepository)
        {
            _unitOfWork = UnitOfWork;
            _companyRepository = CompanyRepository;
        }

        public async Task<string> GetDetail()
        {
            var test = _companyRepository.GetAll();
            var count = await test.CountAsync();
            await _unitOfWork.CommitAsyn();
            return ApiResponse.Ok(test, count);
        }
    }
}