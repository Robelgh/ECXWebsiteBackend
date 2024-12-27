using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.FeedBack_.Request.Command;
using ECX.Website.Application.DTOs.FeedBack;
using ECX.Website.Application.DTOs.FeedBack.Validators;
using ECX.Website.Application.DTOs.Common.Validators;
using ECX.Website.Application.Exceptions;
using ECX.Website.Application.Response;
using ECX.Website.Domain;
using MediatR;

namespace ECX.Website.Application.CQRS.FeedBack_.Handler.Command
{
    public class UpdateFeedBackCommandHandler : IRequestHandler<UpdateFeedBackCommand, BaseCommonResponse>
    {
        private IFeedBackRepository _feedBackRepository;
        private IMapper _mapper;
        public UpdateFeedBackCommandHandler(IFeedBackRepository feedBackRepository, IMapper mapper)
        {
            _feedBackRepository = feedBackRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommonResponse> Handle(UpdateFeedBackCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommonResponse();
            var validator = new FeedBackUpdateDtoValidator();
            // var validationResult = await validator.ValidateAsync(request.FeedBackAnswerFormDto);
         

            var FeedBackDto = _mapper.Map<FeedBackDto>(request.FeedBackAnswerFormDto);
            var flag = await _feedBackRepository.Exists(request.FeedBackAnswerFormDto.Id);

            if (false)
            {
                response.Success = false;
                response.Message = "Update Failed";
               // response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                response.Status = "400";
            }
            else if (flag == false)
            {

                response.Success = false;
           //     response.Message = new NotFoundException(
                   //         nameof(FeedBack), request.FeedBackFormDto.Id).Message.ToString();
                response.Status = "404";
            }
            else 
            {
                var updateData = await _feedBackRepository.GetById(request.FeedBackAnswerFormDto.Id);
                   updateData.Answer = request.FeedBackAnswerFormDto.Answer;
                   updateData.AnsweredBy = request.FeedBackAnswerFormDto.AnsweredBy;
                  // updateData.requestSeen = request.FeedBackAnswerFormDto.requestSeen;
                  //updateData.answerSeen = request.FeedBackAnswerFormDto.answerSeen;
               // _mapper.Map(FeedBackDto, updateData);
                try
                {
                    var data = await _feedBackRepository.Update(updateData);

                    response.Data = _mapper.Map<FeedBackDto>(data);
                    response.Success = true;
                    response.Message = "Updated Successfull";
                    response.Status = "200";
                }
                catch (Exception ex)
                {

                    throw;
                }
         
            }
            return response;
        }
    }
}

