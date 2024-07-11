﻿using FluentValidation.Results;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Communication.Enums;
using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using Journey.Application.UseCases.Trips.Activities.Register;

namespace Journey.Application.UseCases.Activities.Register
{
    public class RegisterActivityFroTripUseCase
    {
        public ResponseActivityJson Execute(Guid tripId, RequestRegisterActivityJson request)
        {
            var dbContext = new JourneyDbContext();
            var trip = dbContext.Trips.Include(trip => trip.Activities).FirstOrDefault();
            Validate(trip, request);

            var entity = new Activity
            {
                Name = request.Name,
                Date = request.Date,
            };

            trip!.Activities.Add(entity);
            dbContext.Trips.Update(trip);
            dbContext.SaveChanges();

            return new ResponseActivityJson
            {
                Date = entity.Date,
                Id = entity.Id,
                Name = entity.Name,
                Status = (Communication.Enums.ActivityStatus)entity.Status,
            };
        }

        private void Validate(Trip? trip, RequestRegisterActivityJson request)
        {
            if (trip is null)
            {
                throw new DirectoryNotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
            }

            var validator = new RegisterActivityValidator();
            var result = validator.Validate(request);
            if ((request.Date >= trip.StartDate && request.Date <= trip.EndDate) == false)
            {
                result.Errors.Add(new ValidationFailure("Date", ResourceErrorMessages.DATE_NOT_WITHIN_TRAVEL_PERIOD));
            }

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }

    }

    
}