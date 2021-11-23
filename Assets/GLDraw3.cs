using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GLDraw3 : MonoBehaviour
{
  public Material mat;
  public Vector2 sb;
  bool sg = false;
  public float by = 0;
  float bx = 0;
  bool motoca = false;
  public float mx = 10;
  public float my = 10;
  float velo = 0.05f;
  bool shoot = false;
  public float mGL = -2;
  public float mGR = 2;
  bool invert;
  public int life = 3;
  public int score = 0;
  public Text tL;
  public Text tS;

  #region Unity Methods
  private void Start()
  {
    tL.text = life.ToString();
    tS.text = score.ToString();
    sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
  }
  private void Update()
  {
    tL.text = life.ToString();
    tS.text = score.ToString();

    sb = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

    if (Input.GetKey(KeyCode.LeftArrow))
    {
      bx -= velo;
    }
    if (Input.GetKey(KeyCode.RightArrow))
    {
      bx += velo;
    }
    if (Input.GetKey(KeyCode.Space))
    {
      shoot = true;
      by += velo;
    }
    if (Input.GetKey(KeyCode.Escape))
    {
      shoot = false;
      by = 0;
    }
    Move();
    Collision();
  }
  private void OnPostRender()
  {
    if (sg)
    {
      BarRight();
      BarGol();
      BarLeft();
      BarBottom();
      Ball();
      // CarMustang();
      Motorcycle(mx, my);
    }
  }
  #endregion

  #region My Methods
  public void SatartGame()
  {
    sg = true;
  }
  public void QuitGame()
  {
    Application.Quit();
    Debug.Log("Game is exiting");
  }
  void Ball()
  {
    GL.PushMatrix();
    mat.SetPass(0);
    GL.Begin(GL.QUADS);
    GL.Color(Color.red);

    GL.Vertex3(bx, by, 0);
    GL.Vertex3(bx, by + 1, 0);
    GL.Vertex3(bx + 1, by + 1, 0);
    GL.Vertex3(bx + 1, by, 0);

    GL.End();
    GL.PopMatrix();
  }
  void BarGol()
  {
    GL.PushMatrix();
    mat.SetPass(0);
    GL.Begin(GL.QUADS);
    GL.Color(Color.yellow);

    GL.Vertex3(sb.x * (-1), sb.y, 0);
    GL.Vertex3(sb.x * (-1), sb.y - 1, 0);
    GL.Vertex3(mGL, sb.y - 1, 0);
    GL.Vertex3(mGL, sb.y, 0);

    GL.Vertex3(mGR, sb.y, 0);
    GL.Vertex3(mGR, sb.y - 1, 0);
    GL.Vertex3(sb.x, sb.y - 1, 0);
    GL.Vertex3(sb.x, sb.y, 0);

    GL.End();
    GL.PopMatrix();
  }
  void BarRight()
  {
    GL.PushMatrix();
    mat.SetPass(0);
    GL.Begin(GL.QUADS);
    GL.Color(Color.yellow);
    GL.Vertex3(sb.x, sb.y * (-1), 0);
    GL.Vertex3(sb.x, sb.y, 0);
    GL.Vertex3(sb.x - 1, sb.y, 0);
    GL.Vertex3(sb.x - 1, sb.y * (-1), 0);
    GL.End();
    GL.PopMatrix();
  }
  void BarLeft()
  {
    GL.PushMatrix();
    mat.SetPass(0);
    GL.Begin(GL.QUADS);
    GL.Color(Color.yellow);
    GL.Vertex3(sb.x * (-1), sb.y * (-1), 0);
    GL.Vertex3(sb.x * (-1), sb.y, 0);
    GL.Vertex3(sb.x * (-1) + 1, sb.y, 0);
    GL.Vertex3(sb.x * (-1) + 1, sb.y * (-1), 0);
    GL.End();
    GL.PopMatrix();
  }
  void BarBottom()
  {
    GL.PushMatrix();
    mat.SetPass(0);
    GL.Begin(GL.QUADS);
    GL.Color(Color.yellow);
    GL.Vertex3(sb.x * (-1), sb.y * (-1), 0);
    GL.Vertex3(sb.x * (-1), sb.y * (-1) + 1, 0);
    GL.Vertex3(sb.x, sb.y * (-1) + 1, 0);
    GL.Vertex3(sb.x, sb.y * (-1), 0);
    GL.End();
    GL.PopMatrix();
  }
  void CarMustang(int carX, int carY)
  {
    Color carColor = Color.white;
    carColor.r = 255;
    carColor.g = 165;
    carColor.b = 0;
    const int carWidth = 6;
    const int carHeight = 13;
    const float wheelHeight = 2.0f;
    const float wheelWidth = 0.5f;

    GL.PushMatrix();
    mat.SetPass(0);

    // #region Preencher Carcaça Carro
    GL.Begin(GL.QUADS);
    GL.Color(carColor);
    GL.Vertex(new Vector3(carX, carY, 0));
    GL.Vertex(new Vector3(carX, carY + carWidth, 0));
    GL.Vertex(new Vector3(carX + carHeight, carY + carWidth, 0));
    GL.Vertex(new Vector3(carX + carHeight, carY, 0));
    GL.End();
    GL.Begin(GL.QUADS);
    GL.Color(carColor);
    GL.Vertex(new Vector3(carX + carHeight, carY + carWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 1.05f), carY + (carWidth / 2), 0));
    GL.Vertex(new Vector3(carX + carHeight, carY, 0));
    GL.Vertex(new Vector3(carX + carHeight, carY, 0));
    GL.End();
    // #endregion Preencher Carcaça Carro
    // #region Preencher Pneus
    GL.Begin(GL.QUADS);
    GL.Color(Color.gray);
    GL.Vertex(new Vector3(carX + (carHeight * 0.25f) - (wheelHeight * 0.5f), carY, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.25f) - (wheelHeight * 0.5f), carY - wheelWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.25f) - (wheelHeight * 0.5f) + wheelHeight, carY - wheelWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.25f) - (wheelHeight * 0.5f) + wheelHeight, carY, 0));
    GL.End();
    GL.Begin(GL.QUADS);
    GL.Color(Color.gray);
    GL.Vertex(new Vector3(carX + (carHeight * 0.25f) - (wheelHeight * 0.5f), carY + carWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.25f) - (wheelHeight * 0.5f), carY + carWidth + wheelWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.25f) - (wheelHeight * 0.5f) + wheelHeight, carY + carWidth + wheelWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.25f) - (wheelHeight * 0.5f) + wheelHeight, carY + carWidth, 0));
    GL.End();
    GL.Begin(GL.QUADS);
    GL.Color(Color.gray);
    GL.Vertex(new Vector3(carX + (carHeight * 0.75f) + (wheelHeight * 0.5f), carY, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.75f) + (wheelHeight * 0.5f), carY - wheelWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.75f) + (wheelHeight * 0.5f) - wheelHeight, carY - wheelWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.75f) + (wheelHeight * 0.5f) - wheelHeight, carY, 0));
    GL.End();
    GL.Begin(GL.QUADS);
    GL.Color(Color.gray);
    GL.Vertex(new Vector3(carX + (carHeight * 0.75f) + (wheelHeight * 0.5f), carY + carWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.75f) + (wheelHeight * 0.5f), carY + carWidth + wheelWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.75f) + (wheelHeight * 0.5f) - wheelHeight, carY + carWidth + wheelWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.75f) + (wheelHeight * 0.5f) - wheelHeight, carY + carWidth, 0));
    GL.End();
    // #endregion Preencher Pneus

    GL.Begin(GL.LINES);
    GL.Color(Color.black);

    // #region Carro                
    // #region Carro#Carcaça            
    GL.Vertex(new Vector3(carX, carY, 0));
    GL.Vertex(new Vector3(carX, carY + carWidth, 0));
    GL.Vertex(new Vector3(carX, carY + carWidth, 0));
    GL.Vertex(new Vector3(carX + carHeight, carY + carWidth, 0));
    GL.Vertex(new Vector3(carX + carHeight, carY + carWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 1.05f), carY + (carWidth / 2), 0));
    GL.Vertex(new Vector3(carX + (carHeight * 1.05f), carY + (carWidth / 2), 0));
    GL.Vertex(new Vector3(carX + carHeight, carY, 0));
    GL.Vertex(new Vector3(carX + carHeight, carY, 0));
    GL.Vertex(new Vector3(carX, carY, 0));
    // #endregion Carro#Carcaça
    // #region Capô
    GL.Vertex(new Vector3(carX + carHeight, carY + carWidth - 0.5f, 0));
    GL.Vertex(new Vector3(carX + carHeight - 3, carY + carWidth - 0.5f, 0));
    GL.Vertex(new Vector3(carX + carHeight - 3, carY + carWidth - 0.5f, 0));
    GL.Vertex(new Vector3(carX + carHeight - 4, carY + carWidth - 2, 0));
    GL.Vertex(new Vector3(carX + carHeight - 4, carY + carWidth - 2, 0));
    GL.Vertex(new Vector3(carX + carHeight - 4, carY + 2, 0));
    GL.Vertex(new Vector3(carX + carHeight - 4, carY + 2, 0));
    GL.Vertex(new Vector3(carX + carHeight - 3, carY + 0.5f, 0));
    GL.Vertex(new Vector3(carX + carHeight - 3, carY + 0.5f, 0));
    GL.Vertex(new Vector3(carX + carHeight, carY + 0.5f, 0));
    // #region Centro Capô
    GL.Vertex(new Vector3(carX + carHeight - 3, carY + carWidth - 2, 0));
    GL.Vertex(new Vector3(carX + carHeight - 2, carY + carWidth - 2, 0));
    GL.Vertex(new Vector3(carX + carHeight - 2, carY + carWidth - 2, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 1.05f) - 2, carY + (carWidth / 2), 0));
    GL.Vertex(new Vector3(carX + (carHeight * 1.05f) - 2, carY + (carWidth / 2), 0));
    GL.Vertex(new Vector3(carX + carHeight - 2, carY + 2, 0));
    GL.Vertex(new Vector3(carX + carHeight - 2, carY + 2, 0));
    GL.Vertex(new Vector3(carX + carHeight - 3, carY + 2, 0));
    // #endregion Centro Capô
    // #endregion Capô
    // #region Porta Malas
    GL.Vertex(new Vector3(carX, carY + carWidth - 0.5f, 0));
    GL.Vertex(new Vector3(carX + 2f, carY + carWidth - 0.5f, 0));
    GL.Vertex(new Vector3(carX + 2f, carY + carWidth - 0.5f, 0));
    GL.Vertex(new Vector3(carX + 2f, carY + 0.5f, 0));
    GL.Vertex(new Vector3(carX + 2f, carY + 0.5f, 0));
    GL.Vertex(new Vector3(carX, carY + 0.5f, 0));
    // #endregion Porta Malas
    // #region Janela dianteira
    GL.Vertex(new Vector3(carX + carHeight - 4.5f, carY + 0.5f, 0));
    GL.Vertex(new Vector3(carX + carHeight - 5.5f, carY + 1.5f, 0));
    GL.Vertex(new Vector3(carX + carHeight - 5.5f, carY + 1.5f, 0));
    GL.Vertex(new Vector3(carX + carHeight - 5.5f, carY + carWidth - 1.5f, 0));
    GL.Vertex(new Vector3(carX + carHeight - 5.5f, carY + carWidth - 1.5f, 0));
    GL.Vertex(new Vector3(carX + carHeight - 4.5f, carY + carWidth - 0.5f, 0));
    GL.Vertex(new Vector3(carX + carHeight - 4.5f, carY + carWidth - 0.5f, 0));
    GL.Vertex(new Vector3(carX + carHeight - 4.5f, carY + 0.5f, 0));
    // #endregion Janela dianteira
    // #region Janela traseira
    GL.Vertex(new Vector3(carX + 2.5f, carY + 1, 0));
    GL.Vertex(new Vector3(carX + 3, carY + 1.5f, 0));
    GL.Vertex(new Vector3(carX + 3, carY + 1.5f, 0));
    GL.Vertex(new Vector3(carX + 3, carY + carWidth - 1.5f, 0));
    GL.Vertex(new Vector3(carX + 3, carY + carWidth - 1.5f, 0));
    GL.Vertex(new Vector3(carX + 2.5f, carY + carWidth - 1, 0));
    GL.Vertex(new Vector3(carX + 2.5f, carY + carWidth - 1, 0));
    GL.Vertex(new Vector3(carX + 2.5f, carY + 1, 0));
    // #endregion Janela traseira
    // #region Janela lateral esquerda
    GL.Vertex(new Vector3(carX + 3, carY + carWidth - 0.5f, 0));
    GL.Vertex(new Vector3(carX + carHeight - 5f, carY + carWidth - 0.5f, 0));
    GL.Vertex(new Vector3(carX + carHeight - 5f, carY + carWidth - 0.5f, 0));
    GL.Vertex(new Vector3(carX + carHeight - 6f, carY + carWidth - 1.3f, 0));
    GL.Vertex(new Vector3(carX + carHeight - 6f, carY + carWidth - 1.3f, 0));
    GL.Vertex(new Vector3(carX + carHeight - 7f, carY + carWidth - 1.3f, 0));
    GL.Vertex(new Vector3(carX + carHeight - 7f, carY + carWidth - 1.3f, 0));
    GL.Vertex(new Vector3(carX + 3, carY + carWidth - 0.5f, 0));
    // #endregion Janela lateral esquerda
    // #region Janela lateral direita
    GL.Vertex(new Vector3(carX + 3, carY + 0.5f, 0));
    GL.Vertex(new Vector3(carX + carHeight - 5f, carY + 0.5f, 0));
    GL.Vertex(new Vector3(carX + carHeight - 5f, carY + 0.5f, 0));
    GL.Vertex(new Vector3(carX + carHeight - 6f, carY + 1.3f, 0));
    GL.Vertex(new Vector3(carX + carHeight - 6f, carY + 1.3f, 0));
    GL.Vertex(new Vector3(carX + carHeight - 7f, carY + 1.3f, 0));
    GL.Vertex(new Vector3(carX + carHeight - 7f, carY + 1.3f, 0));
    GL.Vertex(new Vector3(carX + 3, carY + 0.5f, 0));
    // #endregion Janela lateral direita
    // #region Pneu Dianteiro Direita
    GL.Vertex(new Vector3(carX + (carHeight * 0.75f) + (wheelHeight * 0.5f), carY + carWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.75f) + (wheelHeight * 0.5f), carY + carWidth + wheelWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.75f) + (wheelHeight * 0.5f), carY + carWidth + wheelWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.75f) + (wheelHeight * 0.5f) - wheelHeight, carY + carWidth + wheelWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.75f) + (wheelHeight * 0.5f) - wheelHeight, carY + carWidth + wheelWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.75f) + (wheelHeight * 0.5f) - wheelHeight, carY + carWidth, 0));
    // #endregion Pneu Dianteiro Direita
    // #region Pneu Dianteiro Esquerda
    GL.Vertex(new Vector3(carX + (carHeight * 0.75f) + (wheelHeight * 0.5f), carY, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.75f) + (wheelHeight * 0.5f), carY - wheelWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.75f) + (wheelHeight * 0.5f), carY - wheelWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.75f) + (wheelHeight * 0.5f) - wheelHeight, carY - wheelWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.75f) + (wheelHeight * 0.5f) - wheelHeight, carY - wheelWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.75f) + (wheelHeight * 0.5f) - wheelHeight, carY, 0));
    // #endregion Pneu Dianteiro Esquerda
    // #region Pneu Traseiro Direita
    GL.Vertex(new Vector3(carX + (carHeight * 0.25f) - (wheelHeight * 0.5f), carY + carWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.25f) - (wheelHeight * 0.5f), carY + carWidth + wheelWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.25f) - (wheelHeight * 0.5f), carY + carWidth + wheelWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.25f) - (wheelHeight * 0.5f) + wheelHeight, carY + carWidth + wheelWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.25f) - (wheelHeight * 0.5f) + wheelHeight, carY + carWidth + wheelWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.25f) - (wheelHeight * 0.5f) + wheelHeight, carY + carWidth, 0));
    // #endregion Pneu Traseiro Direita
    // #region Pneu Traseiro Esquerda
    GL.Vertex(new Vector3(carX + (carHeight * 0.25f) - (wheelHeight * 0.5f), carY, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.25f) - (wheelHeight * 0.5f), carY - wheelWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.25f) - (wheelHeight * 0.5f), carY - wheelWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.25f) - (wheelHeight * 0.5f) + wheelHeight, carY - wheelWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.25f) - (wheelHeight * 0.5f) + wheelHeight, carY - wheelWidth, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 0.25f) - (wheelHeight * 0.5f) + wheelHeight, carY, 0));
    // #endregion Pneu Traseiro Esquerda
    // #endregion Carro

    GL.End();
    GL.PopMatrix();
  }
  void Motorcycle(float motorcycleX, float motorcycleY)
  {
    motoca = true;

    GL.PushMatrix();
    mat.SetPass(0);
    GL.Begin(GL.LINES);

    // #region motoca
    // #region contorno motoca
    GL.Vertex(new Vector3(motorcycleX, motorcycleY, 0));
    GL.Vertex(new Vector3(motorcycleX, motorcycleY + 1, 0));
    GL.Vertex(new Vector3(motorcycleX, motorcycleY + 1, 0));
    GL.Vertex(new Vector3(motorcycleX + 1, motorcycleY + 1, 0));
    GL.Vertex(new Vector3(motorcycleX + 1, motorcycleY + 1, 0));
    GL.Vertex(new Vector3(motorcycleX + 5, motorcycleY + (float)2.5, 0));
    GL.Vertex(new Vector3(motorcycleX + 5, motorcycleY + (float)2.5, 0));
    GL.Vertex(new Vector3(motorcycleX + (float)5.5, motorcycleY + 4, 0));
    GL.Vertex(new Vector3(motorcycleX + (float)5.5, motorcycleY + 4, 0));
    GL.Vertex(new Vector3(motorcycleX + 6, motorcycleY + (float)3.8, 0));
    GL.Vertex(new Vector3(motorcycleX + 6, motorcycleY + (float)3.8, 0));
    GL.Vertex(new Vector3(motorcycleX + (float)5.5, motorcycleY + (float)2.5, 0));
    GL.Vertex(new Vector3(motorcycleX + (float)5.5, motorcycleY + (float)2.5, 0));
    GL.Vertex(new Vector3(motorcycleX + (float)9.5, motorcycleY + (float)1.5, 0));
    GL.Vertex(new Vector3(motorcycleX + (float)9.5, motorcycleY + (float)1.5, 0));
    GL.Vertex(new Vector3(motorcycleX + (float)14.5, motorcycleY + 2, 0));
    GL.Vertex(new Vector3(motorcycleX + (float)14.5, motorcycleY + 2, 0));
    GL.Vertex(new Vector3(motorcycleX + 16, motorcycleY + (float)1.5, 0));
    GL.Vertex(new Vector3(motorcycleX + 16, motorcycleY + (float)1.5, 0));
    GL.Vertex(new Vector3(motorcycleX + 17, motorcycleY + (float)1.5, 0));
    GL.Vertex(new Vector3(motorcycleX + 17, motorcycleY + (float)1.5, 0));
    GL.Vertex(new Vector3(motorcycleX + 17, motorcycleY + (float)(-0.5), 0));
    GL.Vertex(new Vector3(motorcycleX + 17, motorcycleY + (float)(-0.5), 0));
    GL.Vertex(new Vector3(motorcycleX + 16, motorcycleY + (float)(-0.5), 0));
    GL.Vertex(new Vector3(motorcycleX + 16, motorcycleY + (float)(-0.5), 0));
    GL.Vertex(new Vector3(motorcycleX + (float)14.5, motorcycleY + (-1), 0));
    GL.Vertex(new Vector3(motorcycleX + (float)14.5, motorcycleY + (-1), 0));
    GL.Vertex(new Vector3(motorcycleX + 16, motorcycleY + (-1), 0));
    GL.Vertex(new Vector3(motorcycleX + 16, motorcycleY + (-1), 0));
    GL.Vertex(new Vector3(motorcycleX + 16, motorcycleY + (float)(-1.8), 0));
    GL.Vertex(new Vector3(motorcycleX + 16, motorcycleY + (float)(-1.8), 0));
    GL.Vertex(new Vector3(motorcycleX + (float)12.5, motorcycleY + (float)(-1.8), 0));
    GL.Vertex(new Vector3(motorcycleX + (float)12.5, motorcycleY + (float)(-1.8), 0));
    GL.Vertex(new Vector3(motorcycleX + (float)9.5, motorcycleY + (float)(-0.5), 0));
    GL.Vertex(new Vector3(motorcycleX + (float)9.5, motorcycleY + (float)(-0.5), 0));
    GL.Vertex(new Vector3(motorcycleX + (float)5.5, motorcycleY + (float)(-1.5), 0));
    GL.Vertex(new Vector3(motorcycleX + (float)5.5, motorcycleY + (float)(-1.5), 0));
    GL.Vertex(new Vector3(motorcycleX + 6, motorcycleY + (float)(-2.8), 0));
    GL.Vertex(new Vector3(motorcycleX + 6, motorcycleY + (float)(-2.8), 0));
    GL.Vertex(new Vector3(motorcycleX + (float)5.5, motorcycleY + (-3), 0));
    GL.Vertex(new Vector3(motorcycleX + (float)5.5, motorcycleY + (-3), 0));
    GL.Vertex(new Vector3(motorcycleX + 5, motorcycleY + (float)(-1.5), 0));
    GL.Vertex(new Vector3(motorcycleX + 5, motorcycleY + (float)(-1.5), 0));
    GL.Vertex(new Vector3(motorcycleX + 1, motorcycleY, 0));
    GL.Vertex(new Vector3(motorcycleX + 1, motorcycleY, 0));
    GL.Vertex(new Vector3(motorcycleX, motorcycleY, 0));
    // #endregion contorno motoca
    // #region linhas internas motoca
    GL.Vertex(new Vector3(motorcycleX + 5, motorcycleY + (float)2.5, 0));
    GL.Vertex(new Vector3(motorcycleX + 5, motorcycleY + (float)(-1.5), 0));

    GL.Vertex(new Vector3(motorcycleX + (float)5.5, motorcycleY + (float)2.5, 0));
    GL.Vertex(new Vector3(motorcycleX + (float)5.5, motorcycleY + (float)(-1.5), 0));

    GL.Vertex(new Vector3(motorcycleX + (float)14.5, motorcycleY + (-1), 0));
    GL.Vertex(new Vector3(motorcycleX + (float)9.5, motorcycleY + (float)(-0.5), 0));

    GL.Vertex(new Vector3(motorcycleX + 5, motorcycleY + (float)2.5, 0));
    GL.Vertex(new Vector3(motorcycleX + 3, motorcycleY + (float)0.5, 0));
    GL.Vertex(new Vector3(motorcycleX + 3, motorcycleY + (float)0.5, 0));
    GL.Vertex(new Vector3(motorcycleX + 5, motorcycleY + (float)(-1.5), 0));
    // #endregion linhas internas motoca
    // #endregion motoca

    GL.End();
    GL.PopMatrix();
  }
  
  void Move()
  {
    if (shoot)
      by += velo;

    if (mGL > sb.x * (-1) && invert)
    {
      mGR -= velo;
      mGL -= velo;
    }
    else
    {
      invert = false;
    }
    if (mGR < sb.x && !invert)
    {
      mGR += velo;
      mGL += velo;
    }
    else
    {
      invert = true;
    }

  }
  void Collision()
  {
    //Trave collision
    if ((by + 1) >= (sb.y - 1) && (bx <= mGL || bx + 1 >= mGR))
    {
      life--;
      by = 0;
      shoot = false;
    }
    //Gol collision
    else if (by >= sb.y)
    {
      velo += 0.05f;
      score++;
      by = 0;
      shoot = false;
    }
    //Motoca collision
    else if (((by + 1) >= (my - 3) && (by - 1) <= (my + 4)) && ((bx + 1) >= (mx) && (bx - 1) <= (my + 17))){
      if (motoca) {
        life--;
        by = 0;
        shoot = false;
      }
    }
  }
  #endregion
}
