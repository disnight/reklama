function formInit() {
    "use strict";

    $('.with-tooltip').tooltip({
        selector: ".input-tooltip"
    });

    /*----------- BEGIN autosize CODE -------------------------*/
    $('#autosize').autosize();
    /*----------- END autosize CODE -------------------------*/

    /*----------- BEGIN inputlimiter CODE -------------------------*/
    $('#limiter').inputlimiter({
        limit: 140,
        remText: 'You only have %n character%s remaining...',
        limitText: 'You\'re allowed to input %n character%s into this field.'
    });

    $('.param-desc').inputlimiter({
        limit: 512,
        remText: 'You only have %n character%s remaining...',
        limitText: 'You\'re allowed to input %n character%s into this field.'
    });
    /*----------- END inputlimiter CODE -------------------------*/

   /*----------- BEGIN chosen CODE -------------------------*/

    $(".chzn-select").chosen();
    $(".chzn-select-deselect").chosen({
        allow_single_deselect: true
    });
    /*----------- END chosen CODE -------------------------*/

    /*----------- BEGIN spinner CODE -------------------------*/

    $('#spin1').spinner();
    $("#spin2").spinner({
        step: 0.01,
        numberFormat: "n"
    });
    $("#spin3").spinner({
        culture: 'en-US',
        min: 5,
        max: 2500,
        step: 25,
        start: 1000,
        numberFormat: "C"
    });
    /*----------- END spinner CODE -------------------------*/

    /*----------- BEGIN uniform CODE -------------------------*/
    $('.uniform').uniform();
    /*----------- END uniform CODE -------------------------*/

    /*----------- BEGIN validVal CODE -------------------------*/
    $('#validVal').validVal();
    /*----------- END validVal CODE -------------------------*/

    

    /*----------- BEGIN toggleButtons CODE -------------------------*/
    // Resets to the regular style
    $('#dimension-switch').bootstrapSwitch('setSizeClass', '');
    // Sets a mini switch
    $('#dimension-switch').bootstrapSwitch('setSizeClass', 'switch-mini');
    // Sets a small switch
    $('#dimension-switch').bootstrapSwitch('setSizeClass', 'switch-small');
    // Sets a large switch
    $('#dimension-switch').bootstrapSwitch('setSizeClass', 'switch-large');
    /*----------- END toggleButtons CODE -------------------------*/

  
}