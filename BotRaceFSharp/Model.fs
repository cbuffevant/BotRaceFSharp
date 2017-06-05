module Model
    type Direction = Left | Down | Right | Up
    let Oposite (d : Direction) =
        match d with
            | Direction.Left -> Direction.Right
            | Direction.Down -> Direction.Up
            | Direction.Right -> Direction.Left
            | Direction.Up -> Direction.Down
    
    type Position = int * int
    let Column p = snd p
    let Row p = fst p

    let Move direction position : Position =
        match direction with
            | Direction.Up     -> (Row position - 1, Column position)
            | Direction.Down   -> (Row position + 1, Column position)
            | Direction.Right  -> (Row position, Column position + 1)
            | Direction.Left   -> (Row position, Column position - 1)

    module Cell =
        type Cell = int
        type Wall = West = 1 | South = 2 | East = 4 | North = 8 | All = 15

        let Create() : Cell =
            int Wall.All

        let HasWall (w : Wall) (cell : Cell) =
            cell &&& int w <> 0

        let IsClosed (cell : Cell) =
            cell = int Wall.All

        let CarveWall (w : Wall) (cell : Cell) : Cell =
            cell &&& ~~~(int w) 

        let ToJson(cell : Cell) =
            cell.ToString("X")
        
        let GetWallInDirection (direction : Direction) : Wall =
            match direction with
                | Direction.Up    -> Wall.North
                | Direction.Down  -> Wall.South
                | Direction.Left  -> Wall.West
                | Direction.Right -> Wall.East

    module Maze =
        open Cell
        type Maze = Cell array array

        let CreateClosed size =
            [| for i in 1..size -> [| for i in 1..size -> Cell.Create() |] |]

        let Width (maze : Maze) = maze.Length
        
        let Height (maze : Maze) = if (Width maze > 0) then maze.[0].Length else 0

        let Home() : Position = (0,0)

        let Exit maze : Position = (Width maze, Height maze)
        
        let private PositionExists (maze : Maze) (p : Position) =
            let row, column = Row p, Column p 
            row >= 0 && row < Height maze && column >= 0 && column < Width maze

        let CellAt position maze : Cell option =
            if (PositionExists maze position) then
                Some maze.[Row position].[Column position]
            else
                None


        let UpdateMaze position cell maze = 
            if (PositionExists maze position) then
                maze.[Row position].[Column position] <- cell

        let Carve (position : Position) (direction : Direction) (maze : Maze) =
            match maze |> CellAt position with
                | Some fromCell ->
                    let newPosition = position |> Move direction
                    match maze |> CellAt newPosition with
                        | Some toCell -> 
                            maze |> UpdateMaze position (fromCell |> CarveWall (GetWallInDirection direction))
                            maze |> UpdateMaze newPosition (toCell |> CarveWall (GetWallInDirection (Oposite direction)))
                            ()
                        | None -> ()
                 | None ->()

        let ToJson (maze : Maze) =
            maze 
                |> Array.fold (fun state row -> Array.concat [state; row]) [||] 
                |> Array.map (fun cell -> cell |> ToJson)
                |> String.concat ""