import 'package:firebase_auth/firebase_auth.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

class HomePage extends StatefulWidget {
  const HomePage({super.key});

  @override
  State<HomePage> createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  DateTime? startDate;
  DateTime? endDate;
  bool loading = false;
  List registros = [];

  final String apiUrl = 'http://192.168.1.3:5001/api/TimeEntry';

  Future<void> _selecionarDataInicio() async {
    final data = await showDatePicker(
      context: context,
      initialDate: startDate ?? DateTime.now(),
      firstDate: DateTime(2020),
      lastDate: DateTime(2100),
    );
    if (data != null) {
      setState(() => startDate = data);
    }
  }

  Future<void> _selecionarDataFim() async {
    final data = await showDatePicker(
      context: context,
      initialDate: endDate ?? DateTime.now(),
      firstDate: DateTime(2020),
      lastDate: DateTime(2100),
    );
    if (data != null) {
      setState(() => endDate = data);
    }
  }

  Future<void> _baterPonto() async {
    setState(() => loading = true);

    try {
      final user = FirebaseAuth.instance.currentUser;
      final token = await user?.getIdToken();

      final response = await http.post(
        Uri.parse(apiUrl),
        headers: {
          'Content-Type': 'application/json',
          'Authorization': 'Bearer $token',
        },
        body: json.encode({'type': 0}),
      );

      if (response.statusCode == 200 || response.statusCode == 201) {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(content: Text('Ponto registrado com sucesso!')),
        );
        _buscarRegistros();
      } else {
        throw Exception('Erro ao registrar ponto');
      }
    } catch (e) {
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(SnackBar(content: Text('Erro: $e')));
    } finally {
      setState(() => loading = false);
    }
  }

  Future<void> _buscarRegistros() async {
    setState(() {
      loading = true;
      registros.clear();
    });

    try {
      final user = FirebaseAuth.instance.currentUser;
      final token = await user?.getIdToken();

      final queryParams = {
        if (startDate != null)
          'StartDate': DateFormat('yyyy-MM-dd').format(startDate!),
        if (endDate != null)
          'EndDate': DateFormat('yyyy-MM-dd').format(endDate!),
      };

      final uri = Uri.parse(apiUrl).replace(queryParameters: queryParams);

      final response = await http.get(
        uri,
        headers: {'Authorization': 'Bearer $token'},
      );

      if (response.statusCode == 200) {
        setState(() => registros = json.decode(response.body));
      } else {
        throw Exception('Erro ao buscar registros');
      }
    } catch (e) {
      ScaffoldMessenger.of(
        context,
      ).showSnackBar(SnackBar(content: Text('Erro: $e')));
    } finally {
      setState(() => loading = false);
    }
  }

  String formatDate(DateTime? date) =>
      date != null ? DateFormat('dd/MM/yyyy').format(date) : 'Selecionar';

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.white,
      appBar: AppBar(
        backgroundColor: const Color(0xFF007AFF),
        title: Row(
          children: const [
            Icon(Icons.access_time, color: Colors.white),
            SizedBox(width: 8),
            Text('Sistema de Ponto', style: TextStyle(color: Colors.white)),
          ],
        ),
        actions: [
          IconButton(
            icon: const Icon(Icons.logout, color: Colors.white),
            onPressed: () {
              FirebaseAuth.instance.signOut();
            },
          ),
        ],
      ),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          children: [
            Row(
              children: [
                Expanded(
                  child: OutlinedButton(
                    onPressed: _selecionarDataInicio,
                    child: Text('Início: ${formatDate(startDate)}'),
                  ),
                ),
                const SizedBox(width: 12),
                Expanded(
                  child: OutlinedButton(
                    onPressed: _selecionarDataFim,
                    child: Text('Fim: ${formatDate(endDate)}'),
                  ),
                ),
              ],
            ),
            const SizedBox(height: 16),
            ElevatedButton(
              onPressed: loading ? null : _baterPonto,
              style: ElevatedButton.styleFrom(
                backgroundColor: const Color(0xFF007AFF),
                minimumSize: const Size(double.infinity, 48),
              ),
              child: const Text(
                'BATER PONTO',
                style: TextStyle(color: Colors.white),
              ),
            ),
            const SizedBox(height: 8),
            ElevatedButton(
              onPressed: loading ? null : _buscarRegistros,
              style: ElevatedButton.styleFrom(
                backgroundColor: const Color(0xFF007AFF),
                minimumSize: const Size(double.infinity, 48),
              ),
              child: const Text(
                'VER REGISTROS',
                style: TextStyle(color: Colors.white),
              ),
            ),
            const SizedBox(height: 24),
            if (loading) const CircularProgressIndicator(),
            if (registros.isNotEmpty)
              Expanded(
                child: ListView.builder(
                  itemCount: registros.length,
                  itemBuilder: (context, index) {
                    final item = registros[index];
                    return Card(
                      child: ListTile(
                        title: Text(item['date'] ?? 'Sem data'),
                        subtitle: Text(
                          'Entrada: ${item['entry'] ?? 'N/A'}\n'
                          'Saída: ${item['exit'] ?? 'N/A'}\n'
                          'Horas trabalhadas: ${item['workedHours'] ?? 'N/A'}',
                        ),
                      ),
                    );
                  },
                ),
              ),
          ],
        ),
      ),
    );
  }
}
