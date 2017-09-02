using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadorSalas : MonoBehaviour
{
	internal static JogadorSalas instancia;

	internal Sala salaAnterior;
	internal Sala salaAtual;

	private void Awake()
	{
		instancia = this;
	}

	private void FixedUpdate()
	{
		DefinirSala();
	}

	private void DefinirSala()
	{
		Vector3 down = transform.TransformDirection(Vector3.down);

		RaycastHit hit;

		if (Physics.Raycast(transform.position, down, out hit, 100, 1 << 8))
		{
			salaAtual = Sala.PegarSala(hit.transform);

			if (salaAnterior != salaAtual)
				AtualizarSalas();

			if (salaAnterior == null)
				EsconderSalas();

			salaAnterior = salaAtual;
		}
	}

	public void EsconderSalas()
	{
		Sala[] salas = FindObjectsOfType<Sala>();

		foreach (Sala sala in salas)
			if (sala != salaAtual)
				sala.OcultarSala();

		AtualizarSalas();
	}

	private void AtualizarSalas()
	{
		if (salaAnterior)
			salaAnterior.gameObject.SetActive(false);

		salaAtual.gameObject.SetActive(true);

		salaAtual.ExibirConexoesSala();
	}
}
