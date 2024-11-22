using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.Fact_.Request.Command;
using ECX.Website.Application.DTOs.Fact;
using ECX.Website.Application.DTOs.Fact.Validators;
using ECX.Website.Application.Exceptions;

using ECX.Website.Application.Response;
using ECX.Website.Domain;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using ECX.Website.Application.DTOs.Common.Validators;

namespace ECX.Website.Application.CQRS.Fact_.Handler.Command
{
    public class CreateFactCommandHandler : IRequestHandler<CreateFactCommand, BaseCommonResponse>
    {
        private IFactRepository _FactRepository;
        private IMapper _mapper;

        public CreateFactCommandHandler(IFactRepository FactRepository, IMapper mapper)
        {
            _FactRepository = FactRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommonResponse> Handle(CreateFactCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommonResponse();
            var validator = new FactCreateDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FactFormDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Faild";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                response.Status = "400";
            }
            else
            {
                try
                {

                        var FactDto = _mapper.Map<FactDto>(request.FactFormDto);
                  

                        Guid FactId;
                        bool flag = true;

                        while (true)
                        {
                            FactId = Guid.NewGuid();
                            flag = await _FactRepository.Exists(FactId);
                            if (flag == false)
                            {
                                FactDto.Id = FactId;
                                break;
                            }
                        }

                        var data = _mapper.Map<Facts>(FactDto);

                        var saveData = await _FactRepository.Add(data);

                        response.Data = _mapper.Map<FactDto>(saveData);
                        response.Success = true;
                        response.Message = "Created Successfully";
                        response.Status = "200";
                    
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = "Creation Failed";
                    response.Errors = new List<string> { ex.Message };
                    response.Status = "400";
                }
            }
            return response;
        }
    }
}
