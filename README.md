# EjerciciosVocali
Repositorio para los ejercicios de evaluacion para Vocali

## Ejercicio 1
Dado un conjunto de N periodos, calcular la suma de estos teniendo en cuenta lo
siguiente:

Un periodo está identificado por un id único, y tiene una hora de inicio (mayor o
igual que 00:00) y una hora de fin (menor o igual que 23:59).
Asumiremos que la hora de inicio siempre será menor que la hora de fin.

La suma de 2 periodos se calcula de la siguiente forma: los periodos solapados
se combinan en uno nuevo, de forma que el resultado final sea otro conjunto de
periodos que no se solapen.

El conjunto de períodos se especificará mediante un fichero CSV donde cada
período vendrá representado por la terna id,ini,fin con formato:
o Id: string. Ej: A
o Ini: string donde se indica una hora en formato mm:ss. Ej: 08:15
o Fin: string donde se indica una hora en formato mm:ss. Ej: 08:15

En el ejemplo siguiente la suma de Periodo 1 y Periodo 2 da como resultado tres
periodos:
-  [id=A, ini=03:00, fin=05:59]
-  [id=AB, ini=06:00, fin=08:00]
-  [id=B, ini=08:01, fin=09:00]

## Ejercicio 2
INVOX Medical dispone de un servicio para la transcripción de ficheros de audio. En
este ejercicio se simulará la existencia de este servicio mediante un mock up con las
siguientes restricciones:

1. Existirán 4 textos predefinidos (cualesquiera) posibles que siempre se
devolverán para una transcripción exitosa sea cual sea el contenido del fichero
MP3. Se creará una función que seleccionará el texto a elegir en función del
contenido del fichero enviado de manera que cualquiera de los 4 textos tenga la
misma probabilidad de ser elegido.

2. El mock up devolverá un error genérico el 5% de las veces que sea invocado.

3. Para un mismo fichero siempre se devolverá el mismo resultado (salvo que se
haya producido un error genérico).

El servicio se publicará como un servicio REST con un único método que aceptará el
fichero a transcribir en formato MP3 y devolverá el texto correspondiente en base a lo
especificado en el párrafo anterior.

## Ejercicio 3
En INVOX Medical los usuarios pueden crear comandos especiales de voz llamados
Sustituciones. Las sustituciones se activarán cuando el reconocedor detecta un
determinado comando de voz. Pueden ser de 3 tipos:

- Sustituciones de texto: al activarse, se escribirá un texto predefinido en la
ventana de texto activa. Ej: “Plantilla endoscopia” -> “Texto de plantilla…”

- Sustituciones de teclado: al activarse, se ejecutará una combinación de teclas
en la ventana de texto activa. Ej: “Negrita” -> Ctrl + B

- Sustituciones compuestas: al activarse, se ejecutará una sucesión de
sustituciones de cualquiera de los 3 tipos.

¿Cómo lo implementarías (diagrama de clases)? ¿Cómo lo almacenarías en base de
datos (diagrama entidad-relación)?

NOTA: Obviar la implementación de la ejecución de las sustituciones de texto y teclado,
bastará con mostrar un mensaje.
