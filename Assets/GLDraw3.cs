using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GLDraw3 : MonoBehaviour
{
  public Material mat;
  public Vector2 sb;
  bool sg = false;
  public float by = -15;
  public float bx = 0;
  bool motoca = false;
  bool carrinho = false;
  bool mustang = false;
  public float bcx = 0; //basciCar
  public float bcy = 18; //basciCar
  public float mcx = -30; //mustangCar
  public float mcy = 10; //mustangCar
  public float amcx = -30; //Alternative mustangCar
  public float amcy = 0; //Alternative mustangCar
  public float mx = 15; //motorcycle
  public float my = 4; //motorcycle
  float velo = 0.05f;
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

    if (Input.GetKey(KeyCode.UpArrow))
    {
      by += velo;
    }
    if (Input.GetKey(KeyCode.DownArrow) && by > (sb.y * (-1) + 4))
    {
      by -= velo;
    }
    if (Input.GetKey(KeyCode.LeftArrow) && bx > (sb.x * (-1) + 2.5f))
    {
      bx -= velo;
    }
    if (Input.GetKey(KeyCode.RightArrow) && bx < (sb.x - 2.5f))
    {
      bx += velo;
    }

    Move();
    Collision();
    MoveVehicles();
  }
  private void OnPostRender()
  {
    if (sg)
    {
      BarObjective();
      BarBottom();
      CarMustang(mcx, mcy);
      CarMustang(amcx, amcy);
      BasicCar(bcx, bcy);
      Motorcycle(mx, my);
      Mouse();
    }
  }
  #endregion

  #region My Methods
  public void StartGame()
  {
    sg = true;
    by = (sb.y * (-1) + 4);
  }
  public void QuitGame()
  {
    // save any game data here
#if UNITY_EDITOR
    // Application.Quit() does not work in the editor so
    // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
    UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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

  void Mouse()
  {
    GL.PushMatrix();
    mat.SetPass(0);

    Color gray = Color.gray;
    Color black = Color.black;
    Color pink = new Color(0.835f, 0.647f, 0.741f);

    float radius = 2f;

    Vector3 center = new Vector3(bx + (0), by + (0), 0);

    GL.Begin(GL.QUADS);
    GL.Color(gray);


    //Preencher cabe??a do rato
    GL.Vertex(new Vector3(bx + (radius - 0.3f), by + (1), 0));
    GL.Vertex(new Vector3(bx + (0), by + (5), 0));

    GL.Vertex(new Vector3(bx + (-radius + 0.3f), by + (1), 0));
    GL.Vertex(new Vector3(bx + (0), by + (5), 0));

    GL.Vertex(new Vector3(bx + (radius - 0.3f), by + (1), 0));
    GL.Vertex(new Vector3(bx + (-radius + 0.3f), by + (1), 0));

    GL.Vertex(new Vector3(bx + (-radius + 0.3f), by + (1), 0));
    GL.Vertex(new Vector3(bx + (radius - 0.3f), by + (1), 0));

    //Preencher corpo do rato
    FillRectangle(1.55f, 1.55f, new Vector3(0, 0, 0));

    FillRectangle(1, 1, new Vector3((1.5f), 0, 0));
    FillRectangle(1, 1, new Vector3((-1.5f), 0, 0));

    GL.Color(black);

    FillRectangle(0.25f, 0.25f, new Vector3((-0.5f), (2.5f), 0));
    FillRectangle(0.25f, 0.25f, new Vector3((0.5f), (2.5f), 0));

    GL.Color(pink);

    FillRectangle(0.25f, 0.25f, new Vector3((0), (5.25f), 0));
    FillRectangle(0.75f, 0.75f, new Vector3((1.5f), 0, 0));
    FillRectangle(0.75f, 0.75f, new Vector3((-1.5f), 0, 0));

    GL.Vertex(new Vector3(bx + (-0.3f), by - 1.55f, 0));
    GL.Vertex(new Vector3(bx + (0), by + (-5), 0));

    GL.Vertex(new Vector3(bx + (0.3f), by - 1.55f, 0));
    GL.Vertex(new Vector3(bx + (0), by + (-5), 0));

    GL.Vertex(new Vector3(bx + (-0.3f), by - 1.55f, 0));
    GL.Vertex(new Vector3(bx + (0.3f), by - 1.55f, 0));

    GL.End();

    GL.Begin(GL.LINES);
    GL.Color(black);

    GL.Vertex(new Vector3(bx + (0.25f), by + (4.25f), 0));
    GL.Vertex(new Vector3(bx + (1.75f), by + (4.5f), 0));

    GL.Vertex(new Vector3(bx + (-0.25f), by + (4.25f), 0));
    GL.Vertex(new Vector3(bx + (-1.75f), by + (4.5f), 0));

    GL.Vertex(new Vector3(bx + (0.25f), by + (4), 0));
    GL.Vertex(new Vector3(bx + (2), by + (4), 0));

    GL.Vertex(new Vector3(bx + (-0.25f), by + (4), 0));
    GL.Vertex(new Vector3(bx + (-2), by + (4), 0));

    GL.Vertex(new Vector3(bx + (0.25f), by + (3.75f), 0));
    GL.Vertex(new Vector3(bx + (1.75f), by + (3.5f), 0));

    GL.Vertex(new Vector3(bx + (-0.25f), by + (3.75f), 0));
    GL.Vertex(new Vector3(bx + (-1.75f), by + (3.5f), 0));

    GL.End();
    GL.PopMatrix();
  }

  private void makeCircle(float radius, Vector3 center)
  {
    for (float t = 0.0f; t < (2 * Mathf.PI); t += 0.001f)
    {
      Vector3 circle = (new Vector3(Mathf.Cos(t) * radius + center.x, Mathf.Sin(t) * radius + center.y, center.z));
      GL.Vertex3(circle.x, circle.y, circle.z);
    }
  }

  private void FillRectangle(float horizontal, float vertical, Vector3 center)
  {

    GL.Vertex(new Vector3(bx + horizontal + center.x, by + vertical + center.y, 0));
    GL.Vertex(new Vector3(bx + horizontal + center.x, by - vertical + center.y, 0));

    GL.Vertex(new Vector3(bx + horizontal + center.x, by - vertical + center.y, 0));
    GL.Vertex(new Vector3(bx - horizontal + center.x, by - vertical + center.y, 0));

    GL.Vertex(new Vector3(bx - horizontal + center.x, by - vertical + center.y, 0));
    GL.Vertex(new Vector3(bx - horizontal + center.x, by + vertical + center.y, 0));

    GL.Vertex(new Vector3(bx - horizontal + center.x, by + vertical + center.y, 0));
    GL.Vertex(new Vector3(bx + horizontal + center.x, by + vertical + center.y, 0));

  }

  void BarObjective()
  {
    GL.PushMatrix();
    mat.SetPass(0);
    GL.Begin(GL.QUADS);
    GL.Color(Color.gray);

    GL.Vertex3(sb.x * (-1), sb.y, 0);
    GL.Vertex3(sb.x * (-1), sb.y - 4, 0);
    GL.Vertex3(sb.x, sb.y - 4, 0);
    GL.Vertex3(sb.x, sb.y, 0);

    GL.End();
    GL.PopMatrix();
  }
  void BarBottom()
  {
    GL.PushMatrix();
    mat.SetPass(0);
    GL.Begin(GL.QUADS);
    GL.Color(Color.gray);
    GL.Vertex3(sb.x * (-1), sb.y * (-1), 0);
    GL.Vertex3(sb.x * (-1), sb.y * (-1) + 4, 0);
    GL.Vertex3(sb.x, sb.y * (-1) + 4, 0);
    GL.Vertex3(sb.x, sb.y * (-1), 0);
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
    if (mGL > sb.x * (-1) && invert)
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
  }
  void CarMustang(float carX, float carY)
  {
    mustang = true;
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

    // #region Preencher Carca??a Carro
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
    // #endregion Preencher Carca??a Carro
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
    // #region Carro#Carca??a            
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
    // #endregion Carro#Carca??a
    // #region Cap??
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
    // #region Centro Cap??
    GL.Vertex(new Vector3(carX + carHeight - 3, carY + carWidth - 2, 0));
    GL.Vertex(new Vector3(carX + carHeight - 2, carY + carWidth - 2, 0));
    GL.Vertex(new Vector3(carX + carHeight - 2, carY + carWidth - 2, 0));
    GL.Vertex(new Vector3(carX + (carHeight * 1.05f) - 2, carY + (carWidth / 2), 0));
    GL.Vertex(new Vector3(carX + (carHeight * 1.05f) - 2, carY + (carWidth / 2), 0));
    GL.Vertex(new Vector3(carX + carHeight - 2, carY + 2, 0));
    GL.Vertex(new Vector3(carX + carHeight - 2, carY + 2, 0));
    GL.Vertex(new Vector3(carX + carHeight - 3, carY + 2, 0));
    // #endregion Centro Cap??
    // #endregion Cap??
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
  void BasicCar(float bCarX, float bCarY)
  {
    carrinho = true;


    const int bCarWidth = 6;
    const int bCarHeight = 13;
    const float bCarWheelHeight = 2.0f;
    const float bCarWheelWidth = 0.5f;

    GL.PushMatrix();
    mat.SetPass(0);

    //cor carca??a
    GL.Begin(GL.QUADS);
    GL.Color(Color.grey);
    GL.Vertex(new Vector3(bCarX, bCarY, 0));
    GL.Vertex(new Vector3(bCarX, bCarY + bCarWidth, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight, bCarY + bCarWidth, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight, bCarY, 0));
    GL.End();
    GL.Begin(GL.QUADS);
    GL.Color(Color.grey);

    GL.Vertex(new Vector3(bCarX + bCarHeight, bCarY, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight, bCarY, 0));
    GL.End();

    GL.Begin(GL.LINES);
    GL.Color(Color.black);


    // #region Carrinho               
    //   basecarro         

    GL.Vertex(new Vector3(bCarX, bCarY, 0));
    GL.Vertex(new Vector3(bCarX, bCarY + bCarWidth, 0));
    GL.Vertex(new Vector3(bCarX, bCarY + bCarWidth, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight, bCarY + bCarWidth, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight, bCarY + bCarWidth, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight, bCarY, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight, bCarY, 0));
    GL.Vertex(new Vector3(bCarX, bCarY, 0));

    //pneus

    //pneus frente


    GL.Vertex(new Vector3(bCarX + (bCarHeight * 0.75f) + (bCarWheelHeight * 0.1f), bCarY + bCarWidth, 0));
    GL.Vertex(new Vector3(bCarX + (bCarHeight * 0.75f) + (bCarWheelHeight * 0.1f), bCarY + bCarWidth + bCarWheelWidth, 0));
    GL.Vertex(new Vector3(bCarX + (bCarHeight * 0.75f) + (bCarWheelHeight * 0.1f), bCarY + bCarWidth + bCarWheelWidth, 0));
    GL.Vertex(new Vector3(bCarX + (bCarHeight * 0.75f) + (bCarWheelHeight * 0.1f) - bCarWheelHeight, bCarY + bCarWidth + bCarWheelWidth, 0));
    GL.Vertex(new Vector3(bCarX + (bCarHeight * 0.75f) + (bCarWheelHeight * 0.1f) - bCarWheelHeight, bCarY + bCarWidth + bCarWheelWidth, 0));
    GL.Vertex(new Vector3(bCarX + (bCarHeight * 0.75f) + (bCarWheelHeight * 0.1f) - bCarWheelHeight, bCarY + bCarWidth, 0));

    GL.Vertex(new Vector3(bCarX + (bCarHeight * 0.75f) + (bCarWheelHeight * 0.1f), bCarY, 0));
    GL.Vertex(new Vector3(bCarX + (bCarHeight * 0.75f) + (bCarWheelHeight * 0.1f), bCarY - bCarWheelWidth, 0));
    GL.Vertex(new Vector3(bCarX + (bCarHeight * 0.75f) + (bCarWheelHeight * 0.1f), bCarY - bCarWheelWidth, 0));
    GL.Vertex(new Vector3(bCarX + (bCarHeight * 0.75f) + (bCarWheelHeight * 0.1f) - bCarWheelHeight, bCarY - bCarWheelWidth, 0));
    GL.Vertex(new Vector3(bCarX + (bCarHeight * 0.75f) + (bCarWheelHeight * 0.1f) - bCarWheelHeight, bCarY - bCarWheelWidth, 0));
    GL.Vertex(new Vector3(bCarX + (bCarHeight * 0.75f) + (bCarWheelHeight * 0.1f) - bCarWheelHeight, bCarY, 0));

    // pneus traseiros
    GL.Vertex(new Vector3(bCarX + (bCarHeight * 0.25f) - (bCarWheelHeight * 0.9f), bCarY + bCarWidth, 0));
    GL.Vertex(new Vector3(bCarX + (bCarHeight * 0.25f) - (bCarWheelHeight * 0.9f), bCarY + bCarWidth + bCarWheelWidth, 0));
    GL.Vertex(new Vector3(bCarX + (bCarHeight * 0.25f) - (bCarWheelHeight * 0.9f), bCarY + bCarWidth + bCarWheelWidth, 0));
    GL.Vertex(new Vector3(bCarX + (bCarHeight * 0.25f) - (bCarWheelHeight * 0.9f) + bCarWheelHeight, bCarY + bCarWidth + bCarWheelWidth, 0));
    GL.Vertex(new Vector3(bCarX + (bCarHeight * 0.25f) - (bCarWheelHeight * 0.9f) + bCarWheelHeight, bCarY + bCarWidth + bCarWheelWidth, 0));
    GL.Vertex(new Vector3(bCarX + (bCarHeight * 0.25f) - (bCarWheelHeight * 0.9f) + bCarWheelHeight, bCarY + bCarWidth, 0));

    GL.Vertex(new Vector3(bCarX + (bCarHeight * 0.25f) - (bCarWheelHeight * 0.9f), bCarY, 0));
    GL.Vertex(new Vector3(bCarX + (bCarHeight * 0.25f) - (bCarWheelHeight * 0.9f), bCarY - bCarWheelWidth, 0));
    GL.Vertex(new Vector3(bCarX + (bCarHeight * 0.25f) - (bCarWheelHeight * 0.9f), bCarY - bCarWheelWidth, 0));
    GL.Vertex(new Vector3(bCarX + (bCarHeight * 0.25f) - (bCarWheelHeight * 0.9f) + bCarWheelHeight, bCarY - bCarWheelWidth, 0));
    GL.Vertex(new Vector3(bCarX + (bCarHeight * 0.25f) - (bCarWheelHeight * 0.9f) + bCarWheelHeight, bCarY - bCarWheelWidth, 0));
    GL.Vertex(new Vector3(bCarX + (bCarHeight * 0.25f) - (bCarWheelHeight * 0.9f) + bCarWheelHeight, bCarY, 0));



    //tampa porta malas e capo
    //mala
    GL.Vertex(new Vector3(bCarX, bCarY + bCarWidth - 0.5f, 0));
    GL.Vertex(new Vector3(bCarX + 1f, bCarY + bCarWidth - 0.5f, 0));
    GL.Vertex(new Vector3(bCarX + 1f, bCarY + bCarWidth - 0.5f, 0));
    GL.Vertex(new Vector3(bCarX + 1f, bCarY + 0.5f, 0));
    GL.Vertex(new Vector3(bCarX + 1f, bCarY + 0.5f, 0));
    GL.Vertex(new Vector3(bCarX, bCarY + 0.5f, 0));
    //capo
    GL.Vertex(new Vector3(bCarX + bCarHeight, bCarY + bCarWidth - 0.5f, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight - 4, bCarY + bCarWidth - 0.5f, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight - 4, bCarY + bCarWidth - 0.5f, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight - 4, bCarY + 0.5f, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight - 4, bCarY + 0.5f, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight, bCarY + 0.5f, 0));


    //Janelas

    //esquerda
    GL.Vertex(new Vector3(bCarX + 3, bCarY + bCarWidth - 0.3f, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight - 5f, bCarY + bCarWidth - 0.3f, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight - 5f, bCarY + bCarWidth - 0.3f, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight - 6f, bCarY + bCarWidth - 1, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight - 6f, bCarY + bCarWidth - 1, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight - 10f, bCarY + bCarWidth - 1, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight - 10f, bCarY + bCarWidth - 1, 0));
    GL.Vertex(new Vector3(bCarX + 3, bCarY + bCarWidth - 0.3f, 0));

    //direita
    GL.Vertex(new Vector3(bCarX + 3, bCarY + 0.3f, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight - 5f, bCarY + 0.3f, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight - 5f, bCarY + 0.3f, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight - 6f, bCarY + 1, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight - 6f, bCarY + 1, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight - 10f, bCarY + 1, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight - 10f, bCarY + 1, 0));
    GL.Vertex(new Vector3(bCarX + 3, bCarY + 0.3f, 0));
    //dianteira
    GL.Vertex(new Vector3(bCarX + bCarHeight - 4.5f, bCarY + 0.5f, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight - 5.5f, bCarY + 1.2f, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight - 5.5f, bCarY + 1.2f, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight - 5.5f, bCarY + bCarWidth - 1.2f, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight - 5.5f, bCarY + bCarWidth - 1.2f, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight - 4.5f, bCarY + bCarWidth - 0.5f, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight - 4.5f, bCarY + bCarWidth - 0.5f, 0));
    GL.Vertex(new Vector3(bCarX + bCarHeight - 4.5f, bCarY + 0.5f, 0));

    //traseira
    GL.Vertex(new Vector3(bCarX + 2, bCarY + 1, 0));
    GL.Vertex(new Vector3(bCarX + 3, bCarY + 1.2f, 0));
    GL.Vertex(new Vector3(bCarX + 3, bCarY + 1.2f, 0));
    GL.Vertex(new Vector3(bCarX + 3, bCarY + bCarWidth - 1.2f, 0));
    GL.Vertex(new Vector3(bCarX + 3, bCarY + bCarWidth - 1.2f, 0));
    GL.Vertex(new Vector3(bCarX + 2, bCarY + bCarWidth - 1, 0));
    GL.Vertex(new Vector3(bCarX + 2, bCarY + bCarWidth - 1, 0));
    GL.Vertex(new Vector3(bCarX + 2, bCarY + 1, 0));


    // #endregion Carrinho

    GL.End();
    GL.PopMatrix();
  }
  void Collision()
  {
    if (life == 0)
    {
      QuitGame();
    }

    //Cal??ada collision
    if (by >= (sb.y - 4))
    {
      score++;
      by = (sb.y * (-1) + 4);
      velo += 0.025f;
    }
    else
    {
      CheckCollision(bcx, bcy, 13, 6, carrinho);
      CheckCollision(mcx, mcy, 13, 6, mustang);
      CheckCollision(amcx, amcy, 13, 6, mustang);
      CheckCollision(mx, my, 17, 5.8f, motoca);
    }
  }

  void CheckCollision(float x, float y, float width, float height, bool hasItem)
  {
    if ((by) <= y && (by) >= (y - height) && (bx) >= x && (bx) <= (x + width))
    {
      if (hasItem)
      {
        life--;
        by = (sb.y * (-1) + 4);
      }
    }
  }
  void MoveVehicles()
  {
    if (bcx < sb.x)
    {
      bcx += 0.01f + velo * 0.9f;
    }
    else
    {
      bcx = -60;
    }

    if (mcx < sb.x)
    {
      mcx += 0.05f + velo * 0.9f;
    }
    else
    {
      mcx = -60;
    }

    if (amcx < sb.x)
    {
      amcx += (0.05f + velo * 0.9f);
    }
    else
    {
      amcx = -60;
    }

    if (mx > sb.x * (-1))
    {
      mx -= 0.025f + (velo * 0.9f);
    }
    else
    {
      mx = 60;
    }

  }

  #endregion
}
