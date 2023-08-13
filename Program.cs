
ANZ.Sky.StatusClient client = new ANZ.Sky.StatusClient();

client.StatusChanged += (e, a) => Console.WriteLine(a.Message);
client.Connect("ws://localhost:5271/ws");

Console.ReadLine();
client.Disconnect();
