module BackTrackingTest

open System
open MazeHelper
open MazeGenerator
open NUnit.Framework

[<Test>]
let ``Test Backtracking maze``() =
    let maze = RecursiveBacktracking.Create 20
    Console.Write( maze |> Draw)
    Assert.Inconclusive()
