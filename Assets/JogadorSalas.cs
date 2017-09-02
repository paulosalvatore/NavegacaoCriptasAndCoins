using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadorSalas : MonoBehaviour
{
	internal static JogadorSalas instancia;

	public float velocidade;
	private new Rigidbody rigidbody;

	internal Sala salaAnterior;
	internal Sala salaAtual;

	private void Awake()
	{
		instancia = this;

		rigidbody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		float h = Input.GetAxis("Horizontal") * velocidade * Time.deltaTime;
		float v = Input.GetAxis("Vertical") * velocidade * Time.deltaTime;

		transform.Translate(
			new Vector3(
				h,
				0,
				v
			)
		);
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
			salaAtual = hit.transform.parent.GetComponent<Sala>();

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
