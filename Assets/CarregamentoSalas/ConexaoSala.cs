using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConexaoSala : MonoBehaviour
{
	internal Sala sala1;
	internal Sala sala2;

	private MeshRenderer meshRenderer;

	private Vector3 rotacaoPadrao;

	private void Awake()
	{
		DefinirSalasConectadas();

		meshRenderer = GetComponent<MeshRenderer>();

		rotacaoPadrao = transform.eulerAngles;

		OcultaConexaoSala();
	}

	private void DefinirSalasConectadas()
	{
		string nomeLimpoSalas =
			gameObject.name
			.Replace("ConexãoSala ", "")
			.Replace("(", "")
			.Replace(")", "");

		string[] salas = nomeLimpoSalas.Split(' ');

		sala1 = Sala.PegarSala(salas[0]);
		sala2 = Sala.PegarSala(salas[1]);

		sala1.AdicionarConexaoSala(this);
		sala2.AdicionarConexaoSala(this);
	}

	private void OnTriggerStay(Collider collider)
	{
		if (collider.CompareTag("Player"))
		{
			ExibirSalas();

			OcultaConexaoSala();
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		if (collider.CompareTag("Player"))
		{
			ExibeConexaoSala();

			JogadorSalas.instancia.EsconderSalas();
		}
	}

	private void ExibirSalas()
	{
		if (!sala1.gameObject.activeSelf)
			sala1.gameObject.SetActive(true);

		if (!sala2.gameObject.activeSelf)
			sala2.gameObject.SetActive(true);
	}

	public void ExibeConexaoSala()
	{
		AtualizarRotacao();

		meshRenderer.enabled = true;
	}

	public void OcultaConexaoSala()
	{
		meshRenderer.enabled = false;
	}

	private void AtualizarRotacao()
	{
		if (JogadorSalas.instancia.salaAtual == null ||
			(JogadorSalas.instancia.salaAtual != sala1 &&
			JogadorSalas.instancia.salaAtual != sala2))
			return;

		transform.eulerAngles = rotacaoPadrao;

		Vector3 up = transform.TransformDirection(Vector3.up);
		Vector3 down = transform.TransformDirection(Vector3.down);
		Vector3 left = transform.TransformDirection(Vector3.left);
		Vector3 right = transform.TransformDirection(Vector3.right);

		RaycastHit hitUp;
		RaycastHit hitDown;
		RaycastHit hitLeft;
		RaycastHit hitRight;

		if (Physics.Raycast(transform.position, up, out hitUp, 10, 1 << 8))
		{
			if (Sala.PegarSala(hitUp.transform).name == JogadorSalas.instancia.salaAtual.name)
				transform.eulerAngles = new Vector3(
					rotacaoPadrao.x,
					180,
					rotacaoPadrao.z
				);
		}

		if (Physics.Raycast(transform.position, down, out hitDown, 10, 1 << 8))
		{
			if (Sala.PegarSala(hitDown.transform).name == JogadorSalas.instancia.salaAtual.name)
				transform.eulerAngles = new Vector3(
					rotacaoPadrao.x,
					0,
					rotacaoPadrao.z
				);
		}

		if (Physics.Raycast(transform.position, left, out hitLeft, 10, 1 << 8))
		{
			if (Sala.PegarSala(hitLeft.transform).name == JogadorSalas.instancia.salaAtual.name)
				transform.eulerAngles = new Vector3(
					rotacaoPadrao.x,
					90,
					rotacaoPadrao.z
				);
		}

		if (Physics.Raycast(transform.position, right, out hitRight, 10, 1 << 8))
		{
			if (Sala.PegarSala(hitRight.transform).name == JogadorSalas.instancia.salaAtual.name)
				transform.eulerAngles = new Vector3(
					rotacaoPadrao.x,
					270,
					rotacaoPadrao.z
				);
		}
	}
}
