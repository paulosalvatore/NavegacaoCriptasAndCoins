using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sala : MonoBehaviour
{
	internal List<ConexaoSala> conexoesSala = new List<ConexaoSala>();

	internal int id;

	private void Awake()
	{
		id = PegarIdSala(gameObject.name);
	}

	public void AdicionarConexaoSala(ConexaoSala conexaoSala)
	{
		if (!conexoesSala.Contains(conexaoSala))
			conexoesSala.Add(conexaoSala);
	}

	public static int PegarIdSala(string nomeSala)
	{
		nomeSala = nomeSala.Replace("Sala (", "").Replace(")", "");

		return int.Parse(nomeSala);
	}

	public static Sala PegarSala(string salaId)
	{
		string nomeSala = string.Format("Sala ({0})", salaId);

		return GameObject.Find(nomeSala).GetComponent<Sala>();
	}

	public void ExibirConexoesSala()
	{
		foreach (ConexaoSala conexaoSala in conexoesSala)
			conexaoSala.ExibeConexaoSala();
	}

	public void OcultarSala()
	{
		if (gameObject.activeSelf)
			gameObject.SetActive(false);

		foreach (ConexaoSala conexaoSala in conexoesSala)
			conexaoSala.OcultaConexaoSala();
	}

	public static Sala PegarSala(Transform objeto)
	{
		while (objeto.parent != null)
			objeto = objeto.parent;

		return objeto.GetComponent<Sala>();
	}
}
