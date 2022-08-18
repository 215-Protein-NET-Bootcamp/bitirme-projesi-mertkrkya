using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using UrunKatalogProjesi.Core.Entities;
using UrunKatalogProjesi.Core.UnitofWork;
using UrunKatalogProjesi.Data.Repositories;
using UrunKatalogProjesi.Service.Exceptions;

namespace UrunKatalogProjesi.Service.Services
{
    public class BaseService<Dto,TEntity> : IBaseService<Dto, TEntity> where TEntity : class
    {
        private readonly IBaseRepository<TEntity> _repository;
        private readonly IUnitofWork _unitofWork;
        protected readonly IMapper _mapper;
        public BaseService(IBaseRepository<TEntity> repository, IUnitofWork unitofWork, IMapper mapper) : base()
        {
            _repository = repository;
            _unitofWork = unitofWork;
            _mapper = mapper;
        }
        public virtual async Task<ResponseEntity> GetAllAsync()
        {
            try
            {
                var allRecord = await _repository.GetAllAsync();
                var mappedResult = _mapper.Map<IEnumerable<TEntity>, IEnumerable<Dto>>(allRecord);
                return new ResponseEntity(mappedResult);
            }
            catch (Exception e)
            {
                throw new Exception("Get All Error");
            }
        }

        public virtual async Task<ResponseEntity> GetByIdAsync(int id)
        {
            try
            {
                var result = await _repository.GetByIdAsync(id);
                var mappedResult = _mapper.Map<TEntity, Dto>(result);
                return new ResponseEntity(mappedResult);
            }
            catch (Exception e)
            {
                throw new ClientException("No Data with ID: "+id);
            }
        }

        public virtual async Task<ResponseEntity> InsertAsync(Dto entity)
        {
            try
            {
                var tempEntity = _mapper.Map<Dto, TEntity>(entity);
                var result = _repository.InsertAsync(tempEntity);
                await _unitofWork.CommitAsync();
                return new ResponseEntity(entity);
            }
            catch (Exception e)
            {
                throw new Exception("Save Error");
            }
        }

        public virtual async Task<ResponseEntity> UpdateAsync(int id, Dto entity)
        {
            try
            {
                var unUpdatedEntity = await _repository.GetByIdAsync(id);
                if (unUpdatedEntity == null)
                {
                    throw new ClientException("No Data");
                }
                var tempEntity = _mapper.Map<Dto, TEntity>(entity);
                _repository.Update(tempEntity);
                await _unitofWork.CommitAsync();
                return new ResponseEntity(entity);
            }
            catch (Exception e)
            {
                throw new Exception("Update Error");
            }
        }

        public virtual async Task<ResponseEntity> DeleteAsync(int id)
        {
            try
            {
                var deleteEntity = await _repository.GetByIdAsync(id);
                if (deleteEntity == null)
                {
                    throw new ClientException("No Data");
                }
                _repository.Delete(deleteEntity);
                _unitofWork.Commit();
                return new ResponseEntity(deleteEntity);
            }
            catch (Exception e)
            {
                throw new Exception("Delete Error");
            }
        }
    }
}
