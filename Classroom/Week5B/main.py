# CSE 381 REPL5B
# DAG Shortest Path

from graph import Graph
from graph import INF
from topo import dag_topological_sort

def dag_shortest_path(graph, start_vertex):
    pass

g = Graph(6)
g.set_label(0, "A");
g.set_label(1, "B");
g.set_label(2, "C");
g.set_label(3, "D");
g.set_label(4, "E");
g.set_label(5, "F");
g.add_directed_edge(0, 1, 5);
g.add_directed_edge(0, 2, 3);
g.add_directed_edge(1, 2, 2);
g.add_directed_edge(1, 3, 6);
g.add_directed_edge(2, 3, 7);
g.add_directed_edge(2, 4, 4);
g.add_directed_edge(2, 5, 2);
g.add_directed_edge(3, 4, -1);
g.add_directed_edge(3, 5, 1);
g.add_directed_edge(4, 5, -2);
(distance, pred) = dag_shortest_path(g,1)
print(distance)
print(pred)
print()
(distance, pred) = dag_shortest_path(g,0)
print(distance)
print(pred)
