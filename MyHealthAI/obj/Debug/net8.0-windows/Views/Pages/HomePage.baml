   M S B A M L   `   `   `  ����E  AMyHealthAI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:MyHealthAI
MyHealthAI  5      [ WLiveChartsCore.SkiaSharpView.WPF, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null}Xclr-namespace:LiveChartsCore.SkiaSharpView.WPF;assembly=LiveChartsCore.SkiaSharpView.WPF LiveChartsCore.SkiaSharpView.WPF 5          MyHealthAI.HomePage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:MyHealthAI     5      hlvcXclr-namespace:LiveChartsCore.SkiaSharpView.WPF;assembly=LiveChartsCore.SkiaSharpView.WPF     5        N� Title$  
MyHealthAI��5
      .��5      � ���#f1f3f5 � RowDefinitions 5   
   �� 5      $��0* �6   �� 5      $
��200* �6   �� 5      $
��300* �6    � ColumnDefinitions 5   
   �� 5      �� 5      $��0* �6   .+�5    
   � ���#f1f3f5$��10q�6$   $��1��60   $��1��6=    5!      �� 5"      $��1* �6#   �� 5#      $��1* �6#   �� 5$      $��1* �6#   �� 5%      $��1* �6#   �� 5&      $��1* �6#   .+�5+      5  /LiveChartsCore.SkiaSharpView.WPF.CartesianChart  $��0��6k   $	��175��6{      Series 6!   �� *Series+   XAxes 6;   �� *XAxes+   YAxes 6S   �� *YAxes+6{     5,      $��1��6m   $	��175��6}    6!   �� *	Series1+ 6<   �� *XAxes+ 6T   �� *YAxes1+6}     5-      $��2��6m   $	��175��6}    6!   �� *	Series2+ 6<   �� *XAxes+ 6T   �� *YAxes2+6}     5.      $��3��6m   $	��175��6}    6!   �� *	Series3+ 6<   �� *XAxes+ 6T   �� *YAxes3+6}     5/      $��4��6m   $	��175��6}    6!   �� *	Series4+ 6<   �� *XAxes+ 6T   �� *YAxes4+6}   51      �� 54   
   $��Sumar 250ml��6   $��715,190,2,15q�56   
   $��1��6    $��20��6-   $��80��69      NewGameButtonStyle#��[  6F   ��55   
   �� *WaterCommand+56   F   �� 58   
   $��Actualizar grafico��6   $��130,190,2,15q�5:   
   $��1��6    $��20��6-   $	��103��69   #��[  6G   ��59   
   �� *UpdateGrafic+5:   G   �� 5>   
   $
��Agua��6   $��Center=�6   $	��Top=�6<   $��1��6T   $��
715,5,2,15q�6a   $
��17.5(�6u   $��ubuntu)�6�   �� 5?   
   $��Grasas��6   $��Center=�6!   $	��Top=�6>   $��1��6V   $��
375,5,2,15q�6c   $
��17.5(�6w   $��ubuntu)�6�   �� 5@   
   $��Carbohidratos��6   $��Center=�6(   $	��Top=�6E   $��1��6]   $��	65,5,2,15q�6j   $
��17.5(�6}   $��ubuntu)�6�   �� 5A   
   $��	Proteinas��6   $��Center=�6$   $	��Top=�6A   $��1��6Y   $��-275,5,2,15q�6f   $
��17.5(�6{   $��ubuntu)�6�   �� 5B   
   $��Calorias��6   $��Center=�6#   $	��Top=�6@   $��1��6X   $��-586,5,2,15q�6e   $
��17.5(�6z   $��ubuntu)�6�     5F   
   $��1��68   $��0,181,415,180q�6E   $��2��6\    6   �� *	Series5+ 6m   �� *XAxes2+�� 5I   
    �� CornerRadius$ 5��6   $��427.5,220,15,15q�6$   
�������$��1q�6P    J� RenderTransformOrigin$ 0.5,0.5&�6d   $��3��6�   z�5J      V� 5K      .~�5L      �� �� 5M       �� AngleX$ 0.157Q�6$   �� 5N      T� 5O      	 T� X$	 0.397Q�6)   5P      ._�5R      �� $��
10,10,10,0q�6   $	��326��6-   $	��Top=�6:   .+�5T      *   $MyHealthAI.PlaceholderTextBoxControl  $��0,5.8q�5U   2   
   PlaceholderText$0
 *Nombre de la comida (Maximo 15 caracteres)��5V   2   ��5T   2   �� *
MealName+ �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnExceptions$
 True�� �� ValidatesOnDataErrors$
 True��5V   2     5X      $��0,5.8q�5Y   2   $
 Calorias (Max 10000)��5Z   2   ��5X   2   �� *
MealKcal+$ PropertyChanged=�$
 True��$
 True��5Z   2     5]      $��0,5.8q�5^   2   $!
 Peso (Opcional (Max 10000))��5_   2   ��5]   2   �� *
MealWeight+$ PropertyChanged=�$
 True��$
 True��5_   2     5a      $��0,5.8q�5b   2   $
 Proteinas (Max 10000)��5c   2   ��5a   2   �� *MealProtein+$ PropertyChanged=�$
 True��$
 True��5c   2     5e      $��0,5.8q�5f   2   $
 Carbohidratos (Max 10000)��5g   2   ��5e   2   �� *MealCarbohydrate+$ PropertyChanged=�$
 True��$
 True��5g   2     5i      $��0,5.9q�5j   2   $
 Grasas (Max 10000)��5k   2   ��5i   2   �� *	MealFat+$ PropertyChanged=�$
 True��$
 True��5k   2   �� 5n       �� DisplayMemberPath$
 Name��5p       �� SelectedValuePath$ ID��5r      $��0,5.8q�5s      ��5n      �� *
MealType+ �� SelectedValue 5o      �� *SelectedMealTypeId+5s      �� 5v      $��Guardar Comida��6   $��0,5.9q�5x      ��5w      �� *SaveMealCommand+5x      �� 5z      $��Eliminar ultima comida��6   $��0,5.9q�5|      ��5{      �� *DeleteMealCommand+5|      �� 5~      -   5        $%��Selelecionar comida de la lista��5~      $��0,5.9q�5�      5�      5�      �� 5�   
   $ 5��6   $��15,0,427.5,15q�6"   
�������$��1q�6L   $ 0.5,0.5&�6`   $��Bottom=�6�   $��2��6�   z�5�      V� 5�      .~�5�      �� �� 5�      $ 0.157Q�6$   �� 5�      T� 5�      $	 0.397Q�6)   5�      ._�5�      �� $��
10,10,10,0q�6   $	��158��6-   $��Bottom=�6:   .+�5�        $	��0,5q�5�   *   $1
 +Nombre del ejercicio (Maximo 15 caracteres)��5�   *   ��5�   2   �� *ExerciseType+$ PropertyChanged=�$
 True��$
 True��5�   *     5�      $	��0,5q�5�   *   $3
 -Duracion en minutos del ejercicio (Max 10000)��5�   *   ��5�   2   �� *DurationInMinutes+$ PropertyChanged=�$
 True��$
 True��5�   *     5�      $	��0,5q�5�   *   $*
 $Calorias quemadas aprox. (Max 10000)��5�   *   ��5�   2   �� *CaloriesBurned+$ PropertyChanged=�$
 True��$
 True��5�   *   �� 5�      $��Registrar Ejercicio��6   $
��0,10q�5�      ��5�      �� *SaveExerciseCommand+5�      �� 5�      $��Eliminar ultimo ejercicio��6   ��5�      �� *DeleteExerciseCommand+5�      5�      5�      5�      