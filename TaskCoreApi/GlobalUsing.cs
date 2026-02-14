global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Mvc;

global using System.ComponentModel.DataAnnotations;
global using System.Diagnostics;
global using System.Threading.RateLimiting;

global using TaskCoreApi.Controllers;
global using TaskCoreApi.Models;
global using TaskCoreApi.Storages;
global using TaskCoreApi.Interfaces;
global using TaskCoreApi.Enums;
global using TaskCoreApi.Middlewares;

global using TaskCoreApi.Dto;
global using TaskCoreApi.Dto.ProjectDto;
global using TaskCoreApi.Dto.TaskItemDto;
global using TaskCoreApi.Dto.UserDto;