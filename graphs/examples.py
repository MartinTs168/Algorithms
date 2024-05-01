graph = [
    [],
    [2, 4],
    [3],
    [1],
    [2],

]

# graph as a list

graph_paths = [
    (1, 2),
    (1, 3),
    (3, 1),

]

# graph as a list of tuples where the edges are what matters

graph_dict = {
    'Sofia': ['Plovdiv', 'Varna'],
    'Plovdiv': ['Sofia', 'Burgas'],
    'Varna': ['Sofia', 'Plovdiv', 'Burgas'],
}

# graph as a dictionary used when the nodes are strings or unordered numbers like: 1, 5, 12, 4, 90
