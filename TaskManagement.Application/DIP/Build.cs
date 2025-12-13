using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.UseCases.Create;
using TaskManagement.Application.UseCases.Delete;
using TaskManagement.Application.UseCases.Get;
using TaskManagement.Application.UseCases.GetAll;
using TaskManagement.Application.UseCases.Update;
using FluentValidation;
using TaskManagement.Application.Validators;
using TaskManagement.Application.UseCases.Create.Input;
using TaskManagement.Application.UseCases.Update.Input;

namespace TaskManagement.Application.DIP
{
    public static class Build
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection service)
        {
            // create all use case dependencies injections here
            service.AddScoped<ICreateTaskUseCase, CreateTaskUseCase>();
            service.AddScoped<IDeleteTaskUseCase, DeleteTaskUseCase>();
            service.AddScoped<IGetAllTasksUseCase, GetAllTasksUseCase>();
            service.AddScoped<IGetByIdUseCase, GetByIdUseCase>();
            service.AddScoped<IUpdateTaskUseCase, UpdateTaskUseCase>();

            // Register FluentValidation validators
            service.AddTransient<IValidator<CreateTaskInput>, CreateTaskInputValidator>();
            service.AddTransient<IValidator<UpdateTaskInput>, UpdateTaskInputValidator>();

            return service;
        }
    }
}
