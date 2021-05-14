using System;
using System.Diagnostics.CodeAnalysis;
using Accord;
using Accord.Fuzzy;


namespace Proyecto
{
    class Program
    {


        static void Main(string[] args)
        {
            InferenceSystem IS;
            
            int estatura = 0, simpatia = 0, inteligencia = 0, complexion=0, tez=0, bellezaE;
            float belleza = 0;
            string bellezaR = "";

            FuzzySet fsBajo = new FuzzySet("Bajo", new TrapezoidalFunction(140, 155, TrapezoidalFunction.EdgeType.Right));
            FuzzySet fsMedio = new FuzzySet("Medio", new TrapezoidalFunction(160, 170, 175));
            FuzzySet fsAlto = new FuzzySet("Alto", new TrapezoidalFunction(180, 190, TrapezoidalFunction.EdgeType.Left));

            LinguisticVariable lvEstatura = new LinguisticVariable("Estatura", 140, 190);
            lvEstatura.AddLabel(fsBajo);
            lvEstatura.AddLabel(fsMedio);
            lvEstatura.AddLabel(fsAlto);


            FuzzySet fsMalGenio = new FuzzySet("MalGenio", new TrapezoidalFunction(0, 3, TrapezoidalFunction.EdgeType.Right));
            FuzzySet fsNeutro = new FuzzySet("Neutro", new TrapezoidalFunction(4, 5, 6));
            FuzzySet fsAgradable = new FuzzySet("Agradable", new TrapezoidalFunction(7, 10, TrapezoidalFunction.EdgeType.Left));
            
            LinguisticVariable lvSimpatia = new LinguisticVariable("Simpatia", 0, 10);
            lvSimpatia.AddLabel(fsMalGenio);
            lvSimpatia.AddLabel(fsNeutro);
            lvSimpatia.AddLabel(fsAgradable);


            FuzzySet fsPocaIntel = new FuzzySet("PocoInteligente", new TrapezoidalFunction(0, 3, TrapezoidalFunction.EdgeType.Right));
            FuzzySet fsNeutraIntel = new FuzzySet("InteligenciaNeutra", new TrapezoidalFunction(4, 5, 6));
            FuzzySet fsAltaIntel = new FuzzySet("MuyInteligente", new TrapezoidalFunction(7, 10, TrapezoidalFunction.EdgeType.Left));
            
            LinguisticVariable lvInteligencia = new LinguisticVariable("Inteligencia", 0, 10);
            lvInteligencia.AddLabel(fsPocaIntel);
            lvInteligencia.AddLabel(fsNeutraIntel);
            lvInteligencia.AddLabel(fsAltaIntel);

            FuzzySet fsComplexionBaja = new FuzzySet("ComplexionBaja", new TrapezoidalFunction(0, 3, TrapezoidalFunction.EdgeType.Right));
            FuzzySet fsComplexionNeutra = new FuzzySet("ComplexionNeutra", new TrapezoidalFunction(4, 5, 6));
            FuzzySet fsComplexionGrande = new FuzzySet("ComplexionGrande", new TrapezoidalFunction(7, 10, TrapezoidalFunction.EdgeType.Left));

            LinguisticVariable lvComplexion = new LinguisticVariable("Complexion", 0, 10);
            lvComplexion.AddLabel(fsComplexionBaja);
            lvComplexion.AddLabel(fsComplexionNeutra);
            lvComplexion.AddLabel(fsComplexionGrande);

            FuzzySet fsTezClara = new FuzzySet("TezClara", new TrapezoidalFunction(0, 3, TrapezoidalFunction.EdgeType.Right));
            FuzzySet fsTezNeutra = new FuzzySet("TezNeutra", new TrapezoidalFunction(4, 5, 6));
            FuzzySet fsTezOscura = new FuzzySet("TezOscura", new TrapezoidalFunction(7, 10, TrapezoidalFunction.EdgeType.Left));


            LinguisticVariable lvTez = new LinguisticVariable("Tez", 0, 10);
            lvTez.AddLabel(fsTezClara);
            lvTez.AddLabel(fsTezNeutra);
            lvTez.AddLabel(fsTezOscura);

           FuzzySet fsBellezaBaja = new FuzzySet("No_Chido", new TrapezoidalFunction(0, 3, TrapezoidalFunction.EdgeType.Right));
            FuzzySet fsBellezaNeutra = new FuzzySet("Chido", new TrapezoidalFunction(4, 5, 6));
            FuzzySet fsBellezaAlta = new FuzzySet("Muy_Chido", new TrapezoidalFunction(7, 10, TrapezoidalFunction.EdgeType.Left));
            
            LinguisticVariable lvBelleza = new LinguisticVariable("Belleza", 0, 10);
            lvBelleza.AddLabel(fsBellezaBaja);
            lvBelleza.AddLabel(fsBellezaNeutra);
            lvBelleza.AddLabel(fsBellezaAlta);


            Console.WriteLine("INGRESA LA ESTATURA (140 a 190 cm)");
            estatura = Convert.ToInt32(Console.ReadLine());

             Console.WriteLine("INGRESA LA SIMPATIA (1=Mal Genio     10=Muy simpático)");
            simpatia = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Ingresa la Inteligencia del 1 al 10 (No creo que deba explicarlo xd))");
            inteligencia = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("¿Qué complexión tiene? 1=Nada en forma       10=En forma B)");
            complexion = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Ingresa la Tez   1=Oscura        10=Blanco");
            tez= Convert.ToInt32(Console.ReadLine());


            Database fuzzyDB = new Database();
            fuzzyDB.AddVariable(lvEstatura);
            fuzzyDB.AddVariable(lvSimpatia);
            fuzzyDB.AddVariable(lvInteligencia);
            fuzzyDB.AddVariable(lvComplexion);
            fuzzyDB.AddVariable(lvTez);
            fuzzyDB.AddVariable(lvBelleza);

           

            IS = new InferenceSystem(fuzzyDB, new CentroidDefuzzifier(1000));

          

            IS.NewRule("Rule 1", "IF Estatura IS Alto  THEN Belleza IS Muy_Chido");
            IS.NewRule("Rule 2", "IF Simpatia IS Agradable  THEN Belleza IS Muy_Chido");
            IS.NewRule("Rule 3", "IF Inteligencia IS MuyInteligente  THEN Belleza IS Muy_Chido");
            IS.NewRule("Rule 4", "IF Complexion IS ComplexionGrande  THEN Belleza IS Muy_Chido");
            IS.NewRule("Rule 5", "IF Tez IS TezOscura  THEN Belleza IS Muy_Chido");

            IS.NewRule("Rule 6", "IF Estatura IS Medio THEN Belleza IS Chido");
            IS.NewRule("Rule 7", "IF Simpatia IS Neutro  THEN Belleza IS Chido");
            IS.NewRule("Rule 8", "IF Inteligencia IS InteligenciaNeutra  THEN Belleza IS Chido");
            IS.NewRule("Rule 9", "IF Complexion IS ComplexionNeutra  THEN Belleza IS Chido");
            IS.NewRule("Rule 10", "IF Tez IS TezNeutra  THEN Belleza IS Chido");

            IS.NewRule("Rule 11", "IF Estatura IS Bajo THEN Belleza IS No_Chido");
            IS.NewRule("Rule 12", "IF Simpatia IS MalGenio  THEN Belleza IS No_Chido");
            IS.NewRule("Rule 13", "IF Inteligencia IS PocoInteligente  THEN Belleza IS No_Chido");
            IS.NewRule("Rule 14", "IF Complexion IS ComplexionBaja  THEN Belleza IS No_Chido");
            IS.NewRule("Rule 15", "IF Tez IS TezClara  THEN Belleza IS No_Chido");
           

            IS.SetInput("Estatura", estatura);
            IS.SetInput("Simpatia", simpatia);
            IS.SetInput("Inteligencia", inteligencia);
            IS.SetInput("Complexion", complexion);
            IS.SetInput("Tez", tez);

            belleza = IS.Evaluate("Belleza");

            
            //belleza.ToString("##0");
            bellezaE = Convert.ToInt32(belleza.ToString("##0"));

            Console.WriteLine("*****************************************************");
            Console.WriteLine(belleza);

            //Console.WriteLine(IS.SetInput("Belleza", belleza));

            //Console.WriteLine(IS.GetLinguisticVariable("Belleza").GetLabel("Muy_Chido").Name);

          switch (bellezaE)
            {

               
                case int n when (n >= 0 && n<=3):
                    bellezaR = IS.GetLinguisticVariable("Belleza").GetLabel("No_Chido").Name.ToString();
                    break;

                case int n when (n>=4 && n<=6):
                    bellezaR = IS.GetLinguisticVariable("Belleza").GetLabel("Chido").Name.ToString();
                    break;

                case int n when (n >= 7 && n <= 10):
                    bellezaR = IS.GetLinguisticVariable("Belleza").GetLabel("Muy_Chido").Name.ToString();
                    break;
                    

            }

            Console.WriteLine(bellezaR);





        }
    }

}
