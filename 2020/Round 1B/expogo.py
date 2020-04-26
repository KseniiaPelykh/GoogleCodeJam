def get_path(x, y: int, prefix: str):
    if x == 0 and y == 0:
        return prefix
    
    abs_sum = abs(x) + abs(y)
    if abs_sum % 2 == 0:
       return "IMPOSSIBLE"

    if abs(x) % 2 == 1:
        if (x != -1 or y != 0):
            path_E = get_path((x - 1) // 2, y // 2, prefix + "E")
        else:
            path_E = prefix + "W" 
        
        if (x != 1 or y != 0):
            path_W = get_path((x + 1) // 2, y // 2, prefix + "W")    
        else:
            path_W = prefix + "E"

        if (path_E != "IMPOSSIBLE" and path_W != "IMPOSSIBLE"):
            if len(path_E) < len(path_W):
                return path_E
            else: 
                return path_W
        elif (path_E == "IMPOSSIBLE"):
            return path_W
        elif (path_W == "IMPOSSIBLE"):
            return path_E

    elif abs(y) % 2 == 1:

        if (y != -1 or x != 0):
            path_N = get_path(x // 2, (y - 1) // 2, prefix + "N")
        else:
            path_N = prefix + "S"
        
        if (y != 1 or x != 0):
            path_S = get_path(x // 2, (y + 1) // 2, prefix + "S")
        else:
            path_S = prefix + "N"
        
        if (path_S != "IMPOSSIBLE" and path_N != "IMPOSSIBLE"):
            if len(path_S) < len(path_N):
                return path_S
            else: 
                return path_N
        elif (path_S == "IMPOSSIBLE"):
            return path_N
        elif (path_N == "IMPOSSIBLE"):
            return path_S

if __name__ == "__main__":
    t = int(input())
    results = list()
    for i in range(0, t):
        values = list(map(int, input().split()))
        path = get_path(values[0], values[1], "")
        results.append(path)
    
    for i in range(0, t):
        print("Case #%d: %s" % (i + 1, results[i]))
