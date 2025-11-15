# Usando Enum en C# con Entity Framework 

Ejemplo simple y práctico para la implementación de datos seguros usando enumeraciones (enum) en C# con Entity Framework Core y MySQL.

## 📋 Tabla de Contenidos

- [¿Qué son los Enums?](#-qué-son-los-enums)
- [Ventajas de usar Enums](#-ventajas-de-usar-enums)
- [Estructura del Proyecto](#-estructura-del-proyecto)
- [Cómo funcionan los Enums con Entity Framework](#-cómo-funcionan-los-enums-con-entity-framework)
- [Requisitos Previos](#-requisitos-previos)
- [Instalación y Configuración](#-instalación-y-configuración)
- [Cómo Usar](#-cómo-usar)
- [Ejemplos de Uso](#-ejemplos-de-uso)
- [Buenas Prácticas](#-buenas-prácticas)

---

## 🎯 ¿Qué son los Enums?

Los **enumeradores (enum)** son un tipo de dato especial en C# que permite definir un conjunto de constantes con nombre. En lugar de usar números "mágicos" o strings que pueden tener errores tipográficos, los enums proporcionan una forma segura y legible de representar valores fijos.

### Ejemplo básico:
```csharp
public enum Genero
{
    Masculino,  // Valor: 0
    Femenino    // Valor: 1
}
```

Por defecto, los enums son números enteros que comienzan desde 0.

---

## ✅ Ventajas de usar Enums

1. **Seguridad de tipos**: El compilador previene valores inválidos
2. **Legibilidad del código**: `Genero.Masculino` es más claro que `0`
3. **IntelliSense**: Autocompletado en el IDE con todas las opciones disponibles
4. **Refactorización fácil**: Cambiar valores en un solo lugar
5. **Menos errores**: No hay typos en strings ni números incorrectos
6. **Rendimiento**: Más eficiente que strings en memoria y comparaciones

---

## 📁 Estructura del Proyecto

```
Ejercicio 1 enum/
│
├── Connection/
│   └── AppDbContext.cs          # Contexto de Entity Framework
│
├── Entity/
│   └── Paciente.cs               # Modelo de datos
│
├── Enum/
│   └── Genero.cs                 # Definición del enum
│
└── Program.cs                    # Punto de entrada de la aplicación
```

### Descripción de archivos:

- **`Genero.cs`**: Define el enum con los valores posibles
- **`Paciente.cs`**: Entidad que usa el enum como propiedad
- **`AppDbContext.cs`**: Configuración de EF Core y conexión a MySQL
- **`Program.cs`**: Lógica principal con ejemplos de uso

---

## 🔄 Cómo funcionan los Enums con Entity Framework

### ⚡ Comportamiento por defecto (como Integer)

**Entity Framework guarda los enums como enteros en la base de datos** sin necesidad de configuración adicional:

```csharp
public enum Genero
{
    Masculino,  // Se guarda como 0 en BD
    Femenino    // Se guarda como 1 en BD
}
```

**En la Base de Datos:**

<img width="360" height="167" alt="image" src="https://github.com/user-attachments/assets/20765a52-5eaf-4748-99e1-00ffa160e89b" />



**En el código C#:**
```csharp
//mostrar por la consola los registros
var paciente = db.Pacientes.Find(1);
Console.WriteLine(paciente.Genero); // Output: Masculino
//En el program se listan todos los registros
foreach (var p in db.Pacientes)
{
    Console.WriteLine($"{p.Id} - {p.Nombre} - {p.Genero}");
}
```

### ✨ La "magia" de Entity Framework

**No necesitas parsear nada**. EF Core hace la conversión automáticamente:

- **Al guardar**: Convierte `Genero.Masculino` → `0` en la BD
- **Al leer**: Convierte `0` en la BD → `Genero.Masculino` en C#
<img width="234" height="95" alt="image" src="https://github.com/user-attachments/assets/8813912f-ac21-4a2d-ac89-d609f5c5bb47" />


### 📝 Conversión opcional a String

Si prefieres guardar el nombre del enum como texto en la BD:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Paciente>()
        .Property(p => p.Genero)
        .HasConversion<string>(); // Guarda "Masculino" y "Femenino" como texto
}
```

**En la Base de Datos:**
```
+----+---------+-----------+-----------+
| Id | Nombre  | Apellido  | Genero    |
+----+---------+-----------+-----------+
| 1  | Juan    | Pérez     | Masculino |
| 2  | María   | Gómez     | Femenino  |
+----+---------+-----------+-----------+
```

**Ventajas de string**: Más legible directamente en la BD
**Desventajas**: Ocupa más espacio y es más lento en consultas

---

## 📦 Requisitos Previos

- .NET 6.0 o superior
- MySQL Server instalado y ejecutándose
- Visual Studio 2022, VS Code o Rider

---

## ⚙️ Instalación y Configuración

### 1. Clonar el repositorio

```bash
git clone https://github.com/HMBC03/Usando-enum-c-.git
cd Usando-enum-c-
```

### 2. Instalar dependencias

El proyecto requiere estos paquetes NuGet:

```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.EntityFrameworkCore.Design
```

O añadir al archivo `.csproj`:

```xml
<ItemGroup>
  <PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.*" />
  <PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="8.0.*" />
  <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.*" />
</ItemGroup>
```

### 3. Configurar la conexión a MySQL

Edita la cadena de conexión en `AppDbContext.cs`:

```csharp
var connectionString = "server=localhost;port=3306;database=ejemplo1;user=root;password=root;";
```

**Ajusta estos valores según tu configuración:**
- `server`: Dirección del servidor MySQL (generalmente `localhost`)
- `port`: Puerto de MySQL (por defecto `3306`)
- `database`: Nombre de la base de datos (se creará automáticamente)
- `user`: Usuario de MySQL
- `password`: Contraseña del usuario

### 4. Restaurar paquetes

```bash
dotnet restore
```

---

## 🚀 Cómo Usar

### Ejecutar el proyecto

```bash
dotnet run
```

### ¿Qué hace el programa?

1. **Elimina** la base de datos si existe (para testing)
2. **Crea** la base de datos y tablas automáticamente
3. **Inserta** 5 pacientes con diferentes géneros
4. **Muestra** los registros guardados en la consola

### Salida esperada:

```
1 - Juan - Masculino
2 - María - Femenino
3 - Carlos - Masculino
4 - Laura - Femenino
5 - Alex - Masculino
```

---

## 💡 Ejemplos de Uso

### Crear un nuevo paciente

```csharp
var nuevoPaciente = new Paciente
{
    Nombre = "Ana",
    Apellido = "Martínez",
    Genero = Genero.Femenino  // Usamos el enum directamente
};

db.Pacientes.Add(nuevoPaciente);
db.SaveChanges();
```

### Consultar pacientes por género

```csharp
// Buscar todos los pacientes masculinos
var hombres = db.Pacientes
    .Where(p => p.Genero == Genero.Masculino)
    .ToList();

foreach (var p in hombres)
{
    Console.WriteLine($"{p.Nombre} {p.Apellido}");
}
```

### Actualizar el género de un paciente

```csharp
var paciente = db.Pacientes.Find(1);
if (paciente != null)
{
    paciente.Genero = Genero.Femenino;
    db.SaveChanges();
}
```

### Contar pacientes por género

```csharp
var totalMasculinos = db.Pacientes.Count(p => p.Genero == Genero.Masculino);
var totalFemeninos = db.Pacientes.Count(p => p.Genero == Genero.Femenino);

Console.WriteLine($"Masculinos: {totalMasculinos}");
Console.WriteLine($"Femeninos: {totalFemeninos}");
```

### Validar valores antes de crear

```csharp
// Esto NO compila - el enum previene valores inválidos
var paciente = new Paciente
{
    Nombre = "Pedro",
    Genero = "Otro"  // ❌ ERROR DE COMPILACIÓN
};

// Forma correcta
var paciente = new Paciente
{
    Nombre = "Pedro",
    Genero = Genero.Masculino  // ✅ Seguro y válido
};
```

---

## 🎓 Buenas Prácticas

### 1. Nombres claros y descriptivos

```csharp
// ✅ BUENO
public enum EstadoCivil
{
    Soltero,
    Casado,
    Divorciado,
    Viudo
}

// ❌ MALO
public enum Estado
{
    S,
    C,
    D,
    V
}
```

### 2. Usa PascalCase para enums y sus valores

```csharp
// ✅ BUENO
public enum TipoDeSangre
{
    APositivo,
    ANegativo,
    BPositivo,
    BNegativo
}
```

### 3. Organiza enums en carpeta separada

Mantén todos los enums en una carpeta `Enum/` o `Enumerations/` para facilitar su localización.

### 4. Documenta valores especiales

```csharp
public enum EstadoPaciente
{
    Activo = 1,
    Inactivo = 2,
    [Obsolete("Usar Inactivo en su lugar")]
    Suspendido = 3
}
```

### 5. Define el tipo subyacente si es necesario

```csharp
// Para enums con muchos valores (más de 255)
public enum CodigoPostal : int
{
    // ...
}

// Para optimizar memoria si hay pocos valores
public enum Prioridad : byte
{
    Baja = 1,
    Media = 2,
    Alta = 3
}
```

### 6. Usa Flags para valores combinables

```csharp
[Flags]
public enum Permisos
{
    Ninguno = 0,
    Leer = 1,
    Escribir = 2,
    Eliminar = 4,
    TodosLosPermisos = Leer | Escribir | Eliminar
}

// Uso:
var permisos = Permisos.Leer | Permisos.Escribir;
```

### 7. No uses enum para valores que pueden cambiar

❌ **MALO** - Los países pueden cambiar:
```csharp
public enum Pais
{
    Argentina,
    Colombia,
    Mexico
}
```

✅ **BUENO** - Usa una tabla en la BD:
```csharp
public class Pais
{
    public int Id { get; set; }
    public string Nombre { get; set; }
}
```

---

## 🔧 Troubleshooting

### Error: "No se puede conectar a MySQL"

Verifica que:
- MySQL esté ejecutándose
- Las credenciales sean correctas
- El puerto sea el correcto (3306 por defecto)

### Error: "Database already exists"

El código usa `EnsureDeleted()` para testing. En producción, elimina esa línea y usa migraciones:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## 📄 Licencia

Este proyecto está bajo la licencia MIT. Ver el archivo [LICENSE.txt](LICENSE.txt) para más detalles.

---

## 👨‍💻 Autor

**HMBC03**

---

## 📚 Recursos Adicionales

- [Documentación oficial de Enums en C#](https://learn.microsoft.com/es-es/dotnet/csharp/language-reference/builtin-types/enum)
- [Entity Framework Core](https://learn.microsoft.com/es-es/ef/core/)
- [Pomelo MySQL Provider](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)

---

⭐ Si este proyecto te fue útil, considera darle una estrella en GitHub
