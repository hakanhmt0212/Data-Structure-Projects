import networkx as nx
import matplotlib.pyplot as plt

graph_1 = nx.DiGraph()

graph_1.add_node(0)
graph_1.add_node(1)
graph_1.add_node(2)
graph_1.add_node(3)
graph_1.add_node(4)

graph_1.add_edge(0, 1, weight=5.0)
graph_1.add_edge(0, 2, weight=3.0)
graph_1.add_edge(0, 4, weight=2.0)
graph_1.add_edge(1, 2, weight=2.0)
graph_1.add_edge(1, 3, weight=6.0)
graph_1.add_edge(2, 1, weight=1.0)
graph_1.add_edge(2, 3, weight=2.0)
graph_1.add_edge(4, 1, weight=6.0)
graph_1.add_edge(4, 2, weight=10.0)
graph_1.add_edge(4, 3, weight=4.0)

graph_1.nodes[0]['pos'] = (0, 0)
graph_1.nodes[1]['pos'] = (-2, 2)
graph_1.nodes[2]['pos'] = (-4, 1)
graph_1.nodes[3]['pos'] = (-4, -1)
graph_1.nodes[4]['pos'] = (-2, -2)

node_pos  = nx.get_node_attributes(graph_1, 'pos')
arc_weight = nx.get_edge_attributes(graph_1, 'weight')

node_color = ['white' for node in graph_1.nodes()]
edge_color = ['black' for edge in graph_1.edges()]

nx.draw_networkx(graph_1, node_pos, node_color=node_color, node_size=450)
nx.draw_networkx_edges(graph_1, node_pos , edge_color= edge_color)
nx.draw_networkx_edge_labels(graph_1, node_pos, edge_labels=arc_weight)
plt.show()

sp = nx.dijkstra_path(graph_1,source = 4, target = 1)
print("Node 4 to Node 1")
print(sp)
sp = nx.dijkstra_path(graph_1,source = 4, target = 2)
print("Node 4 to Node 2")
print(sp)
sp = nx.dijkstra_path(graph_1,source = 4, target = 3)
print("Node 4 to Node 3")
print(sp)

graph_1.remove_node(3)
graph_1.remove_nodes_from("spam")

print("3. NODE SİLİNDİKTEN SONRA")

node_pos  = nx.get_node_attributes(graph_1, 'pos')
arc_weight = nx.get_edge_attributes(graph_1, 'weight')

node_color = ['white' for node in graph_1.nodes()]
edge_color = ['black' for edge in graph_1.edges()]

nx.draw_networkx(graph_1, node_pos, node_color=node_color, node_size=450)
nx.draw_networkx_edges(graph_1, node_pos , edge_color= edge_color)
nx.draw_networkx_edge_labels(graph_1, node_pos, edge_labels=arc_weight)
plt.show()

sp = nx.dijkstra_path(graph_1,source = 4, target = 1)
print("Node 4 to Node 1")
print(sp)
sp = nx.dijkstra_path(graph_1,source = 4, target = 2)
print("Node 4 to Node 2")
print(sp)