module MazeHelper
open Model
open Model.Maze
open Model.Cell
open System.Text

let Draw(maze : Maze) : string =
    let graph = new StringBuilder()
    let height = Maze.Height maze
    let width = Maze.Width maze
    
    " " |> graph.Append |> ignore
    [1..width * 2 - 1] 
        |> List.map (fun _ -> "_") 
        |> String.concat ""
        |> graph.AppendLine |> ignore
    
    for row in 0..height - 1 do
        graph.Append("|") |> ignore
        for column in 0..width - 1 do
            match maze |> CellAt (row, column) with
                | Some cell ->
                    (if (cell |> HasWall Wall.South) then  "_" else " ") |> graph.Append |> ignore
                    (if (cell |> HasWall Wall.East)  then  "|" else " ") |> graph.Append |> ignore
                | None -> ()
        graph.AppendLine() |> ignore
    graph.ToString()