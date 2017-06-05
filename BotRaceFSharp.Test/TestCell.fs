module TestCell

open NUnit.Framework
open Model
open Model.Cell

[<Test>]
let ``On creation cell is closed``() =
    let cell = Cell.Create()
    Assert.That(cell |> IsClosed, Is.True)

[<Test>]
let ``Is closed if has all the walls``() =
    let cell = Cell.Create()
    Assert.That (
        [Wall.North; Wall.South; Wall.East; Wall.West] 
        |> List.forall (fun w -> cell |> HasWall w), 
        Is.True)
    Assert.That(cell |> IsClosed, Is.True)

[<Test>]
let ``Is not closed if miss a wall``() =
    let cell = Cell.Create()
    let cell2 = cell |> CarveWall Wall.North
    Assert.That(cell2 |> IsClosed, Is.False)

[<Test>]
let ``On carve on direction only that wall disapears``() =
    let walls = [Wall.North; Wall.East; Wall.West; Wall.South]
    walls 
    |> Seq.iter (fun carvedWall ->
        let cell = Cell.Create() |> CarveWall carvedWall
        Assert.That(cell |> HasWall carvedWall, Is.False)
        walls   |> List.except  [carvedWall] 
                |> Seq.iter (fun testWall ->
                    Assert.That(cell |> HasWall testWall, Is.True)))

[<Test>]
let ``To Json closed digit``() =
    let cell = Cell.Create()
    Assert.That(cell |> IsClosed, Is.True)
    Assert.That(cell |> ToJson, Is.EqualTo "F" )

[<Test>]
let ``To Json 1 digit``() =
    Assert.That(
        Cell.Create()
            |> CarveWall Wall.North
            |> ToJson,
        Is.EqualTo "7")

[<Test>]
let ``To Json open``() =
    Assert.That(
        Cell.Create()
            |> CarveWall Wall.North
            |> CarveWall Wall.East
            |> CarveWall Wall.South
            |> CarveWall Wall.West
            |> ToJson,
        Is.EqualTo "0")