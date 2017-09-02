using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sala : MonoBehaviour
{
	internal List<ConexaoSala> conexoesSala = new List<ConexaoSala>();

	internal float id;

	private void Awake()
	{
		id = PegarIdSala(gameObject.name);
	}

	public void AdicionarConexaoSala(ConexaoSala conexaoSala)
	{
		if (!conexoesSala.Contains(conexaoSala))
			conexoesSala.Add(conexaoSala);
	}

	public static float PegarIdSala(string nomeSala)
	{
		nomeSala = nomeSala.Replace("Sala (", "").Replace(")", "");

		return float.Parse(nomeSala);
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
}
