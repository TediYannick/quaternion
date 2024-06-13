using UnityEngine;

public class QuaternionCustom
{
    public float a, b, c, d;

    public QuaternionCustom(float a, float b, float c, float d)
    {
        this.a = a;
        this.b = b;
        this.c = c;
        this.d = d;
    }

    public static QuaternionCustom operator +(QuaternionCustom q1, QuaternionCustom q2)
    {
        return new QuaternionCustom(q1.a + q2.a, q1.b + q2.b, q1.c + q2.c, q1.d + q2.d);
    }

    public static QuaternionCustom operator *(QuaternionCustom q1, QuaternionCustom q2)
    {
        float newA = q1.a * q2.a - q1.b * q2.b - q1.c * q2.c - q1.d * q2.d;
        float newB = q1.a * q2.b + q1.b * q2.a + q1.c * q2.d - q1.d * q2.c;
        float newC = q1.a * q2.c - q1.b * q2.d + q1.c * q2.a + q1.d * q2.b;
        float newD = q1.a * q2.d + q1.b * q2.c - q1.c * q2.b + q1.d * q2.a;
        return new QuaternionCustom(newA, newB, newC, newD);
    }

    public QuaternionCustom Conjugate()
    {
        return new QuaternionCustom(a, -b, -c, -d);
    }

    public float Norm()
    {
        return Mathf.Sqrt(a * a + b * b + c * c + d * d);
    }

    public QuaternionCustom Normalize()
    {
        float norm = Norm();
        return new QuaternionCustom(a / norm, b / norm, c / norm, d / norm);
    }

    public static float DotProduct(QuaternionCustom q1, QuaternionCustom q2)
    {
        return q1.a * q2.a + q1.b * q2.b + q1.c * q2.c + q1.d * q2.d;
    }

    public static QuaternionCustom CrossProduct(QuaternionCustom q1, QuaternionCustom q2)
    {
        return new QuaternionCustom(0,
                                    q1.c * q2.d - q1.d * q2.c,
                                    q1.d * q2.b - q1.b * q2.d,
                                    q1.b * q2.c - q1.c * q2.b);
    }
    
    public Matrix4x4 ToMatrix()
    {
        float xx = b * b;
        float yy = c * c;
        float zz = d * d;
        float xy = b * c;
        float xz = b * d;
        float yz = c * d;
        float wx = a * b;
        float wy = a * c;
        float wz = a * d;

        Matrix4x4 matrix = new Matrix4x4();
        matrix.m00 = 1.0f - 2.0f * (yy + zz);
        matrix.m01 = 2.0f * (xy - wz);
        matrix.m02 = 2.0f * (xz + wy);
        matrix.m03 = 0.0f;
        
        matrix.m10 = 2.0f * (xy + wz);
        matrix.m11 = 1.0f - 2.0f * (xx + zz);
        matrix.m12 = 2.0f * (yz - wx);
        matrix.m13 = 0.0f;
        
        matrix.m20 = 2.0f * (xz - wy);
        matrix.m21 = 2.0f * (yz + wx);
        matrix.m22 = 1.0f - 2.0f * (xx + yy);
        matrix.m23 = 0.0f;
        
        matrix.m30 = 0.0f;
        matrix.m31 = 0.0f;
        matrix.m32 = 0.0f;
        matrix.m33 = 1.0f;

        return matrix;
    }

    public static QuaternionCustom FromMatrix(Matrix4x4 matrix)
    {
        QuaternionCustom q = new QuaternionCustom(
            0.5f * Mathf.Sqrt(1 + matrix.m00 - matrix.m11 - matrix.m22),
            0.5f * Mathf.Sqrt(1 - matrix.m00 + matrix.m11 - matrix.m22),
            0.5f * Mathf.Sqrt(1 - matrix.m00 - matrix.m11 + matrix.m22),
            0.5f * Mathf.Sqrt(1 + matrix.m00 + matrix.m11 + matrix.m22)
        );

        q.a = 0.25f * (matrix.m21 - matrix.m12);
        q.b = 0.25f * (matrix.m02 - matrix.m20);
        q.c = 0.25f * (matrix.m10 - matrix.m01);
        q.d = 0.25f * (matrix.m30 - matrix.m03);

        return q.Normalize();
    }

    public static QuaternionCustom QuaternionFromRotationMatrix(Matrix4x4 matrix)
    {
        float trace = matrix.m00 + matrix.m11 + matrix.m22;
        float s;
        float w, x, y, d;

        if (trace > 0)
        {
            s = 0.5f / Mathf.Sqrt(trace + 1.0f);
            w = 0.25f / s;
            x = (matrix.m21 - matrix.m12) * s;
            y = (matrix.m02 - matrix.m20) * s;
            d = (matrix.m10 - matrix.m01) * s;
        }
        else
        {
        if (matrix.m00 > matrix.m11 && matrix.m00 > matrix.m22)
            {
                s = 2.0f * Mathf.Sqrt(1.0f + matrix.m00 - matrix.m11 - matrix.m22);
                w = (matrix.m21 - matrix.m12) / s;
                x = 0.25f * s;
                y = (matrix.m01 + matrix.m10) / s;
                d = (matrix.m02 + matrix.m20) / s;
            }
        else if (matrix.m11 > matrix.m22)
        {
            s = 2.0f * Mathf.Sqrt(1.0f + matrix.m11 - matrix.m00 - matrix.m22);
            w = (matrix.m02 - matrix.m20) / s;
            x = (matrix.m01 + matrix.m10) / s;
            y = 0.25f * s;
            d = (matrix.m12 + matrix.m21) / s;
        }
        else
        {
            s = 2.0f * Mathf.Sqrt(1.0f + matrix.m22 - matrix.m00 - matrix.m11);
            w = (matrix.m10 - matrix.m01) / s;
            x = (matrix.m02 + matrix.m20) / s;
            y = (matrix.m12 + matrix.m21) / s;
            d = 0.25f * s;
        }
    }

    return new QuaternionCustom(w, x, y, d).Normalize();
}

public static Matrix4x4 RotationMatrixFromQuaternion(QuaternionCustom q)
{
    return q.ToMatrix();
}


}
