module TodosTests

open System.Net.Http
open System.Text
open Microsoft.AspNetCore.TestHost
open Xunit
open Newtonsoft.Json
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open System
open System.IO
open HttpFunc
open Todos

let shouldContains actual expected = Assert.Contains(actual, expected) 
let shouldEqual expected actual = Assert.Equal(expected, actual)
let shouldNotNull expected = Assert.NotNull(expected)

let createHost() =
    WebHostBuilder()
        .UseContentRoot(Directory.GetCurrentDirectory()) 
        .UseEnvironment("Test")
        .Configure(Action<IApplicationBuilder> App.configureApp)
        .ConfigureServices(Action<IServiceCollection> App.configureServices)


[<Fact>]
let ``GET /todos should respond empty`` () =
    use server = new TestServer(createHost()) 
    use client = server.CreateClient()

    get client "todos"
    |> ensureSuccess
    |> readText
    |> shouldEqual "[]"

