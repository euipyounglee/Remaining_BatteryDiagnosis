   M S B A M L   `   `   `  ����I  EBatteryControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:BatteryControl.View.ChannelBatteryControl.View.Channel  5	      D @SharedLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullH2clr-namespace:SharedLib.Defines;assembly=SharedLibSharedLib.Defines 5
      +    %BatteryControl.View.Channel.StartView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      S OPrism.Wpf, Version=8.0.0.1909, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59$prismhttp://prismlibrary.com/  5      5local)clr-namespace:BatteryControl.View.Channel  5	      ?define2clr-namespace:SharedLib.Defines;assembly=SharedLib  5
      -   5        $��uc��5        � DataContext  5      0   *BatteryControl.View.Channel.StartViewModel  5      *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5      +  %Microsoft.Xaml.Behaviors.EventTrigger  5         	EventName$ Loaded��6   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5      -  'Prism.Interactivity.InvokeCommandAction     Command 6   ��  �� Path$ LoadedCommand�   CommandParameter 6F   ��  �� ElementName$ uc��5      .��5      � .+�5      �� $��Center=�6   $��Center=�6(   ��6C   �� $  ChangeProgressStateCommand���5      *  $SharedLib.Defines.ProgressStateTypes 5      CheckConnections=�62   ��5      �� 5       �� 
TargetType" ��6   .�5       �� 	 �� Property	 � ��
 �� Value$
 0q�6!   �� 5!      	 � ��$
 Stretch=�64   �� 5"      	 � ��$
 Stretch=�62   �� 5#      	 � ��
 5$      �� 5%   	    �� 
TargetType" ��6   .R�5&   
   ��  ��Name$
 IMG1��6   $	��397��6   $	��206��6*   $>��8/BatteryControl;component/Resources/btn_start_normal.png��67    �� Triggers 5'   
   P� 5(       P� Property � �� P� Value . . �5)      ��  �� 
TargetName$
 IMG1��6   	 � ��$>
 8/BatteryControl;component/Resources/btn_start_active.png��67   5*      5,   
   5.      5/      51      52      54      